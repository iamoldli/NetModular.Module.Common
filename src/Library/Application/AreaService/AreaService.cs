using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Common.Application.AreaService.ViewModels;
using NetModular.Module.Common.Domain.Area;
using NetModular.Module.Common.Domain.Area.Models;
using NetModular.Module.Common.Infrastructure;
using NetModular.Module.Common.Infrastructure.AreaCrawlingHandler;
using NetModular.Module.Common.Infrastructure.Repositories;

namespace NetModular.Module.Common.Application.AreaService
{
    public class AreaService : IAreaService
    {
        private readonly ICacheHandler _cache;
        private readonly IMapper _mapper;
        private readonly IAreaRepository _repository;
        private readonly CommonDbContext _dbContext;

        public AreaService(IMapper mapper, IAreaRepository repository, ICacheHandler cache, CommonDbContext dbContext)
        {
            _mapper = mapper;
            _repository = repository;
            _cache = cache;
            _dbContext = dbContext;
        }

        public async Task<IResultModel> Query(AreaQueryModel model)
        {
            var result = new QueryResultModel<AreaEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(AreaAddModel model)
        {
            var entity = _mapper.Map<AreaEntity>(model);
            if (await _repository.Exists(entity))
            {
                return ResultModel.HasExists;
            }

            entity.Pinyin = NPinyin.Pinyin.GetPinyin(entity.Name);
            entity.Jianpin = NPinyin.Pinyin.GetInitials(entity.Name);

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _repository.DeleteAsync(id))
            {
                await ClearCache(entity);
                return ResultModel.Success();
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Edit(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<AreaUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(AreaUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.Exists(entity))
            {
                return ResultModel.HasExists;
            }
            
            entity.Pinyin = NPinyin.Pinyin.GetPinyin(entity.Name);
            entity.Jianpin = NPinyin.Pinyin.GetInitials(entity.Name);

            if (await _repository.UpdateAsync(entity))
            {
                await ClearCache(entity);
                return ResultModel.Success();
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> CrawlInsert(IList<AreaCrawlingModel> list)
        {
            using (var uow = _dbContext.NewUnitOfWork())
            {
                foreach (var m in list)
                {
                    var entity = _mapper.Map<AreaEntity>(m);
                    await CrawlingInsert(entity, m.Children, uow);
                }

                uow.Commit();
            }

            return ResultModel.Success();
        }

        private async Task CrawlingInsert(AreaEntity entity, List<AreaCrawlingModel> children, IUnitOfWork uow)
        {
            if (await _repository.AddAsync(entity, uow))
            {
                foreach (var child in children)
                {
                    var childEntity = _mapper.Map<AreaEntity>(child);
                    childEntity.Level = entity.Level + 1;
                    childEntity.ParentId = entity.Id;
                    await CrawlingInsert(childEntity, child.Children, uow);
                }
            }
        }

        public async Task<IResultModel<IList<AreaEntity>>> QueryChildren(string parentCode)
        {
            var result = new ResultModel<IList<AreaEntity>>();
            var cacheKey = CacheKeys.AREA + parentCode;
            if (!_cache.TryGetValue(cacheKey, out IList<AreaEntity> list))
            {
                var parentId = 0;
                if (parentCode.NotNull() && parentCode != "0")
                {
                    var entity = await _repository.GetByCode(parentCode);
                    if (entity != null)
                    {
                        parentId = entity.Id;
                    }
                }

                list = await _repository.QueryChildren(parentId);
                await _cache.SetAsync(cacheKey, list);
            }

            return result.Success(list);
        }

        private async Task ClearCache(AreaEntity entity)
        {
            if (entity.ParentId > 0)
            {
                var parent = await _repository.GetAsync(entity.ParentId);
                if (parent != null)
                {
                    await _cache.RemoveAsync(CacheKeys.AREA + parent.Code);
                }
            }
        }
    }
}
