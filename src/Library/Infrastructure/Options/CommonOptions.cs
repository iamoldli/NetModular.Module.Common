using NetModular.Lib.Utils.Core.Options;

namespace NetModular.Module.Common.Infrastructure.Options
{
    /// <summary>
    /// 通用模块配置项
    /// </summary>
    public class CommonOptions : IModuleOptions
    {
        /// <summary>
        /// 附件上传路径
        /// </summary>
        public string AttachmentPath { get; set; }

        /// <summary>
        /// 字典缓存是否启用
        /// </summary>
        public bool DictCacheEnabled { get; set; }
    }
}
