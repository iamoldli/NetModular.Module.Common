using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Common.Application.DictGroupService.ViewModels;
using NetModular.Module.Common.Domain.DictGroup;

namespace NetModular.Module.Common.Application.DictGroupService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DictGroupAddModel, DictGroupEntity>();
            cfg.CreateMap<DictGroupEntity, DictGroupUpdateModel>();
            cfg.CreateMap<DictGroupUpdateModel, DictGroupEntity>();
        }
    }
}
