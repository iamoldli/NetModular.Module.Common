using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Common.Application.DictService;
using NetModular.Module.Common.Application.DictService.ViewModels;
using NetModular.Module.Common.Domain.Dict.Models;

namespace NetModular.Module.Common.Web.Controllers
{
    [Description("字典管理")]
    public class DictController : ModuleController
    {
        private readonly IDictService _service;

        public DictController(IDictService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] DictQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(DictAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired] Guid id)
        {
            return await _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired] Guid id)
        {
            return await _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(DictUpdateModel model)
        {
            return _service.Update(model);
        }
    }
}
