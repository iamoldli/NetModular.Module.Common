using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Common.Application.DictItemService.ResultModels;
using NetModular.Module.Common.Application.DictService.ViewModels;
using NetModular.Module.Common.Domain.Dict;
using NetModular.Module.Common.Domain.Dict.Models;
using NetModular.Module.Common.Domain.DictItem;
using NetModular.Module.Common.Infrastructure;

namespace NetModular.Module.Common.Application.DictService
{
    public class DictService : IDictService
    {
        private readonly IMapper _mapper;
        private readonly IDictRepository _repository;
        private readonly IDictItemRepository _itemRepository;
        private readonly CommonOptions _options;
        private readonly ICacheHandler _cacheHandler;

        public DictService(IMapper mapper, IDictRepository repository, ICacheHandler cacheHandler, IDictItemRepository itemRepository, CommonOptions options)
        {
            _mapper = mapper;
            _repository = repository;
            _cacheHandler = cacheHandler;
            _itemRepository = itemRepository;
            _options = options;
        }

        public async Task<IResultModel> Query(DictQueryModel model)
        {
            var result = new QueryResultModel<DictEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(DictAddModel model)
        {
            var entity = _mapper.Map<DictEntity>(model);
            if (await _repository.Exists(entity))
            {
                return ResultModel.HasExists;
            }

            var result = await _repository.AddAsync(entity);
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Delete(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _itemRepository.ExistsDict(entity.GroupCode, entity.Code))
                return ResultModel.Failed("请先删除关联的数据项");

            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                await _cacheHandler.RemoveAsync($"{CacheKeys.DictSelect}{entity.GroupCode.ToUpper()}_{entity.Code.ToUpper()}");
            }
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<DictUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(DictUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.Exists(entity))
            {
                return ResultModel.HasExists;
            }

            var result = await _repository.UpdateAsync(entity);
            if (result)
            {
                await _cacheHandler.RemoveAsync($"{CacheKeys.DictSelect}{entity.GroupCode.ToUpper()}_{entity.Code.ToUpper()}");
            }
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select(string group, string code)
        {
            if (group.IsNull() || code.IsNull())
                return ResultModel.Failed("请指定分组和编码");

            List<OptionResultModel> result;
            var key = string.Format(CacheKeys.DictSelect, group.ToUpper(), code.ToUpper());
            if (_options.DictCacheEnabled)
            {
                result = await _cacheHandler.GetAsync<List<OptionResultModel>>(key);
                if (result != null)
                    return ResultModel.Success(result);
            }

            var list = await _itemRepository.QueryChildren(group, code);
            result = list.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Value,
                Data = new
                {
                    m.Id,
                    m.Name,
                    m.Value,
                    m.Extend,
                    m.Icon,
                    m.Level
                }
            }).ToList();

            if (_options.DictCacheEnabled)
            {
                await _cacheHandler.SetAsync(key, result);
            }

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Tree(string group, string code)
        {
            if (group.IsNull() || code.IsNull())
                return ResultModel.Failed("请指定分组和编码");

            TreeResultModel<string, DictItemTreeResultModel> tree;
            var key = string.Format(CacheKeys.DictTree, group.ToUpper(), code.ToUpper());
            if (_options.DictCacheEnabled)
            {
                tree = await _cacheHandler.GetAsync<TreeResultModel<string, DictItemTreeResultModel>>(key);
                if (tree != null)
                    return ResultModel.Success(tree);
            }

            var dict = await _repository.GetByCode(group, code);
            if (dict == null)
                return ResultModel.Failed("字典不存在");

            tree = new TreeResultModel<string, DictItemTreeResultModel>
            {
                Id = "",
                Label = dict.Name,
                Path = { dict.Name },
                Item = new DictItemTreeResultModel()
            };
            var list = await _itemRepository.QueryAll(group, code);
            tree.Children = ResolveTree(list, tree);

            if (_options.DictCacheEnabled)
            {
                await _cacheHandler.SetAsync(key, tree);
            }

            return ResultModel.Success(tree);
        }

        private List<TreeResultModel<string, DictItemTreeResultModel>> ResolveTree(IList<DictItemEntity> all, TreeResultModel<string, DictItemTreeResultModel> parent)
        {
            return all.Where(m => m.ParentId == parent.Item.Id).OrderBy(m => m.Sort).Select(m =>
            {
                var node = new TreeResultModel<string, DictItemTreeResultModel>
                {
                    Id = m.Value,
                    Label = m.Name,
                    Item = new DictItemTreeResultModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Extend = m.Extend,
                        Icon = m.Icon,
                        Level = m.Level,
                        ParentId = m.ParentId,
                        Sort = m.Sort,
                        Value = m.Value
                    }
                };
                node.Item.IdList.AddRange(parent.Item.IdList);
                node.Item.IdList.Add(m.Value);

                node.Path.AddRange(parent.Path);
                node.Path.Add(node.Label);
                node.Children = ResolveTree(all, node);
                return node;
            }).ToList();
        }
    }
}
