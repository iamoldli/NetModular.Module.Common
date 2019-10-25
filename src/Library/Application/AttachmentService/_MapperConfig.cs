using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Common.Application.AttachmentService.ResultModels;
using NetModular.Module.Common.Domain.Attachment;

namespace NetModular.Module.Common.Application.AttachmentService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AttachmentEntity, AttachmentUploadResultModel>();
        }
    }
}
