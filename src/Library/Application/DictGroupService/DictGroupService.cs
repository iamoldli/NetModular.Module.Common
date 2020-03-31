using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Module.Common.Application.DictGroupService.ViewModels;
using NetModular.Module.Common.Domain.DictGroup;
using NetModular.Module.Common.Domain.DictGroup.Models;

namespace NetModular.Module.Common.Application.DictGroupService
{
    public class DictGroupService : IDictGroupService
    {
        private readonly IMapper _mapper;
        private readonly IDictGroupRepository _repository;

        public DictGroupService(IMapper mapper, IDictGroupRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(DictGroupQueryModel model)
        {
            var result = new QueryResultModel<DictGroupEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(DictGroupAddModel model)
        {
            var entity = _mapper.Map<DictGroupEntity>(model);
            if (await _repository.ExistsCode(entity.Code))
            {
                return ResultModel.HasExists;
            }

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<DictGroupUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(DictGroupUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.ExistsCode(entity.Code, entity.Id))
            {
                return ResultModel.HasExists;
            }

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select()
        {
            var list = await _repository.GetAllAsync();
            var result = list.OrderBy(m => m.Sort).Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Code
            });

            return ResultModel.Success(result);
        }
    }
}
