using NetModular.Lib.Options.Abstraction;

namespace NetModular.Module.Common.Infrastructure
{
    /// <summary>
    /// 通用模块配置项
    /// </summary>
    public class CommonOptions : IModuleOptions
    {
        /// <summary>
        /// 附件上传路径
        /// </summary>
        [ModuleOptionDefinition("附件上传路径")]
        public string AttachmentPath { get; set; }

        /// <summary>
        /// 启用字典缓存
        /// </summary>
        [ModuleOptionDefinition("启用字典缓存")]
        public bool DictCacheEnabled { get; set; } = true;
    }
}
