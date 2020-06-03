using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Common.Application.AreaService.ViewModels;
using NetModular.Module.Common.Domain.Area;
using NetModular.Module.Common.Infrastructure.AreaCrawlingHandler;

namespace NetModular.Module.Common.Application.AreaService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AreaAddModel, AreaEntity>();
            cfg.CreateMap<AreaEntity, AreaUpdateModel>();
            cfg.CreateMap<AreaUpdateModel, AreaEntity>();
            cfg.CreateMap<AreaCrawlingModel, AreaEntity>();
        }
    }
}
