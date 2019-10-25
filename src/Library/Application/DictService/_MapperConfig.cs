using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Common.Application.DictService.ViewModels;
using NetModular.Module.Common.Domain.Dict;

namespace NetModular.Module.Common.Application.DictService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DictAddModel, DictEntity>();
            cfg.CreateMap<DictEntity, DictUpdateModel>();
            cfg.CreateMap<DictUpdateModel, DictEntity>();
        }
    }
}
