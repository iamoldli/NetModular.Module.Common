using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Module.Common.Application.MediaTypeService;
using NetModular.Module.Common.Application.MediaTypeService.ViewModels;
using NetModular.Module.Common.Domain.MediaType.Models;

namespace NetModular.Module.Common.Web.Controllers
{
    [Description("多媒体管理")]
    public class MediaTypeController : ModuleController
    {
        private readonly IMediaTypeService _service;

        public MediaTypeController(IMediaTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]MediaTypeQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(MediaTypeAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]int id)
        {
            return _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired]int id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(MediaTypeUpdateModel model)
        {
            return _service.Update(model);
        }

    }
}
