using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Module.Common.Application.DictItemService;
using NetModular.Module.Common.Application.DictItemService.ViewModels;
using NetModular.Module.Common.Domain.DictItem.Models;

namespace NetModular.Module.Common.Web.Controllers
{
    [Description("字典数据项管理")]
    public class DictItemController : ModuleController
    {
        private readonly IDictItemService _service;

        public DictItemController(IDictItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] DictItemQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(DictItemAddModel model)
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
        public Task<IResultModel> Update(DictItemUpdateModel model)
        {
            return _service.Update(model);
        }
    }
}
