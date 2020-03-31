using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Module.Common.Application.AreaService;
using NetModular.Module.Common.Application.AreaService.ViewModels;
using NetModular.Module.Common.Domain.Area;
using NetModular.Module.Common.Domain.Area.Models;

namespace NetModular.Module.Common.Web.Controllers
{
    [Description("区划代码管理")]
    public class AreaController : ModuleController
    {
        private readonly IAreaService _service;

        public AreaController(IAreaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] AreaQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(AreaAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired] int id)
        {
            return _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired] int id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(AreaUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpGet]
        [Description("查询子节点")]
        [Common]
        public Task<IResultModel<IList<AreaEntity>>> QueryChildren(string parentCode)
        {
            return _service.QueryChildren(parentCode);
        }
    }
}
