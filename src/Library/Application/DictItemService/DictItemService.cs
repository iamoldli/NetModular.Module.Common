using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Common.Application.DictItemService.ViewModels;
using NetModular.Module.Common.Domain.DictItem;
using NetModular.Module.Common.Domain.DictItem.Models;
using NetModular.Module.Common.Infrastructure;

namespace NetModular.Module.Common.Application.DictItemService
{
    public class DictItemService : IDictItemService
    {
        private readonly IMapper _mapper;
        private readonly IDictItemRepository _repository;
        private readonly CommonOptions _options;
        private readonly ICacheHandler _cacheHandler;

        public DictItemService(IDictItemRepository repository, IMapper mapper, ICacheHandler cacheHandler, CommonOptions options)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheHandler = cacheHandler;
            _options = options;
        }

        public async Task<IResultModel> Query(DictItemQueryModel model)
        {
            var result = new QueryResultModel<DictItemEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(DictItemAddModel model)
        {
            if (model.ParentId < 1 && (model.GroupCode.IsNull() || model.DictCode.IsNull()))
            {
                return ResultModel.Failed("请选择所属分组和字典");
            }

            var entity = _mapper.Map<DictItemEntity>(model);

            if (model.ParentId > 0)
            {
                var parent = await _repository.GetAsync(model.ParentId);
                if (parent == null)
                    return ResultModel.Failed("父节点不存在");

                entity.GroupCode = parent.GroupCode;
                entity.DictCode = parent.DictCode;
                entity.Level = ++parent.Level;
            }

            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed("数据项名称或值已存在");
            }

            var result = await _repository.AddAsync(entity);
            if (result)
            {
                await ClearCache(entity.GroupCode, entity.DictCode);
                return ResultModel.Success(entity.Id);
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Delete(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _repository.ExistsChild(id))
                return ResultModel.Failed("请先删除子节点");

            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                await ClearCache(entity.GroupCode, entity.DictCode);
            }
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<DictItemUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(DictItemUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed("数据项名称或值已存在");
            }

            var result = await _repository.UpdateAsync(entity);
            if (result)
            {
                await ClearCache(entity.GroupCode, entity.DictCode);
            }
            return ResultModel.Result(result);
        }

        private async Task ClearCache(string group, string code)
        {
            if (_options.DictCacheEnabled)
            {
                var selectKey = string.Format(CacheKeys.DictSelect, group.ToUpper(), code.ToUpper());
                var treeKey = string.Format(CacheKeys.DictTree, group.ToUpper(), code.ToUpper());
                await _cacheHandler.RemoveAsync(selectKey);
                await _cacheHandler.RemoveAsync(treeKey);
            }
        }
    }
}
