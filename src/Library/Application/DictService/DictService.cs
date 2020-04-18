using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstractions;
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
        private readonly ICacheHandler _cacheHandler;
        private readonly IConfigProvider _configProvider;

        public DictService(IMapper mapper, IDictRepository repository, ICacheHandler cacheHandler, IDictItemRepository itemRepository, IConfigProvider configProvider)
        {
            _mapper = mapper;
            _repository = repository;
            _cacheHandler = cacheHandler;
            _itemRepository = itemRepository;
            _configProvider = configProvider;
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
                await _cacheHandler.RemoveAsync($"{CacheKeys.DICT_SELECT}:{entity.GroupCode.ToUpper()}_{entity.Code.ToUpper()}");
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
                await _cacheHandler.RemoveAsync($"{CacheKeys.DICT_SELECT}:{entity.GroupCode.ToUpper()}_{entity.Code.ToUpper()}");
            }
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select(string group, string code)
        {
            if (group.IsNull() || code.IsNull())
                return ResultModel.Failed("请指定分组和编码");

            var config = _configProvider.Get<CommonConfig>();
            var key = $"{CacheKeys.DICT_SELECT}:{group.ToUpper()}_{code.ToUpper()}";
            if (config.DictCacheEnabled && _cacheHandler.TryGetValue(key, out List<OptionResultModel> result))
            {
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

            if (config.DictCacheEnabled)
            {
                await _cacheHandler.SetAsync(key, result);
            }

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Tree(string group, string code)
        {
            if (group.IsNull() || code.IsNull())
                return ResultModel.Failed("请指定分组和编码");

            var config = _configProvider.Get<CommonConfig>();
            var key = $"{CacheKeys.DICT_TREE}:{group.ToUpper()}_{code.ToUpper()}";
            if (config.DictCacheEnabled && _cacheHandler.TryGetValue(key, out TreeResultModel<int, DictItemTreeResultModel> root))
            {
                return ResultModel.Success(root);
            }

            var dict = await _repository.GetByCode(group, code);
            if (dict == null)
                return ResultModel.Failed("字典不存在");

            root = new TreeResultModel<int, DictItemTreeResultModel>
            {
                Id = 0,
                Label = dict.Name,
                Item = new DictItemTreeResultModel
                {
                    Name = dict.Name
                }
            };

            var all = await _itemRepository.QueryAll(group, code);
            root.Children = ResolveTree(all);

            if (config.DictCacheEnabled)
            {
                await _cacheHandler.SetAsync(key, root);
            }

            return ResultModel.Success(root);
        }

        private List<TreeResultModel<int, DictItemTreeResultModel>> ResolveTree(IList<DictItemEntity> all, int parentId = 0)
        {
            return all.Where(m => m.ParentId == parentId).OrderBy(m => m.Sort).Select(m =>
            {
                var node = new TreeResultModel<int, DictItemTreeResultModel>
                {
                    Id = m.Id,
                    Label = m.Name,
                    Value = m.Value,
                    Item = _mapper.Map<DictItemTreeResultModel>(m),
                    Children = ResolveTree(all, m.Id)
                };

                return node;
            }).ToList();
        }
    }
}
