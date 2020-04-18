using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Config.Abstractions.Impl;
using NetModular.Lib.Utils.Mvc.Extensions;
using NetModular.Lib.Utils.Mvc.Helpers;
using NetModular.Module.Common.Application.AttachmentService;
using NetModular.Module.Common.Application.AttachmentService.ViewModels;
using NetModular.Module.Common.Domain.Attachment.Models;

namespace NetModular.Module.Common.Web.Controllers
{
    [Description("附件表管理")]
    public class AttachmentController : ModuleController
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IAttachmentService _service;
        private readonly FileUploadHelper _fileUploadHelper;
        private readonly IConfigProvider _configProvider;
        public AttachmentController(IAttachmentService service, ILoginInfo loginInfo, FileUploadHelper fileUploadHelper, IConfigProvider configProvider)
        {
            _service = service;
            _loginInfo = loginInfo;
            _fileUploadHelper = fileUploadHelper;
            _configProvider = configProvider;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]AttachmentQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("上传")]
        [Common]
        public async Task<IResultModel> Upload([FromForm]AttachmentUploadModel model, IFormFile formFile)
        {
            model.AccountId = _loginInfo.AccountId;
            var config = _configProvider.Get<PathConfig>();
            var uploadModel = new FileUploadModel
            {
                Request = Request,
                FormFile = formFile,
                RootPath = config.UploadPath,
                Module = "Common",
                Group = Path.Combine("Attachment", model.Module, model.Group)
            };

            //附件存储路径/Common/Attachment/{所属模块名称}/{所属分组模块}
            var result = await _fileUploadHelper.Upload(uploadModel);

            if (result.Successful)
            {
                var resultModel = await _service.Upload(model, result.Data);
                if (resultModel.Successful)
                {
                    var url = Request.GetHost($"/common/attachment/download/{resultModel.Data.Id}");
                    resultModel.Data.Url = new Uri(url).ToString();
                    return ResultModel.Success(resultModel);
                }
            }

            return ResultModel.Failed("上传失败");
        }

        [HttpGet("{id:guid}")]
        [Description("下载")]
        [Common]
        public async Task<IActionResult> Download([BindRequired]Guid id)
        {
            var result = await _service.Download(id, _loginInfo.AccountId);
            if (result.Successful)
            {
                var file = result.Data;
                return PhysicalFile(file.FilePath, file.MediaType, file.Name, true);
            }

            return new JsonResult(result);
        }

        [HttpGet("{id:guid}")]
        [Description("导出")]
        [Common]
        public async Task<IActionResult> Export([BindRequired]Guid id)
        {
            var result = await _service.Download(id, _loginInfo.AccountId);
            if (result.Successful)
            {
                var file = result.Data;
                return PhysicalFile(file.FilePath, file.MediaType, file.Name, true);
            }

            return new JsonResult(result);
        }
    }
}
