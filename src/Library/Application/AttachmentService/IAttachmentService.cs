using System;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Files;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Common.Application.AttachmentService.ResultModels;
using NetModular.Module.Common.Application.AttachmentService.ViewModels;
using NetModular.Module.Common.Domain.Attachment.Models;

namespace NetModular.Module.Common.Application.AttachmentService
{
    /// <summary>
    /// 附件表服务
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(AttachmentQueryModel model);

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        Task<IResultModel<AttachmentUploadResultModel>> Upload(AttachmentUploadModel model, FileInfo fileInfo);

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IResultModel<FileDownloadModel>> Download(Guid id, Guid accountId);
    }
}
