using NetModular.Lib.Options.Abstraction;

namespace NetModular.Module.Common.Infrastructure
{
    /// <summary>
    /// 通用模块配置项
    /// </summary>
    public class CommonOptions : IModuleOptions
    {
        /// <summary>
        /// 启用字典缓存
        /// </summary>
        [ModuleOptionDefinition("启用字典缓存")]
        public bool DictCacheEnabled { get; set; } = true;

        public IModuleOptions Copy()
        {
            return new CommonOptions
            {
                DictCacheEnabled = DictCacheEnabled
            };
        }
    }
}
