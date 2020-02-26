using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Common.Application.DictItemService.ResultModels;
using NetModular.Module.Common.Application.DictItemService.ViewModels;
using NetModular.Module.Common.Domain.DictItem;

namespace NetModular.Module.Common.Application.DictItemService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DictItemAddModel, DictItemEntity>();
            cfg.CreateMap<DictItemEntity, DictItemUpdateModel>();
            cfg.CreateMap<DictItemUpdateModel, DictItemEntity>();

            cfg.CreateMap<DictItemEntity, DictItemTreeResultModel>();
        }
    }
}
