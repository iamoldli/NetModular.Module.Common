using NetModular.Lib.Config.Abstractions;

namespace NetModular.Module.Common.Infrastructure
{
    /// <summary>
    /// 通用模块配置
    /// </summary>
    public class CommonConfig : IConfig
    {
        /// <summary>
        /// 启用字典缓存
        /// </summary>
        public bool DictCacheEnabled { get; set; } = true;
    }
}
