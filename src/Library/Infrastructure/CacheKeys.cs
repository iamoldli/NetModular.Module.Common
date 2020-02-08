using System.ComponentModel;

namespace NetModular.Module.Common.Infrastructure
{
    /// <summary>
    /// 缓存键
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// 区划代码
        /// </summary>
        [Description("区划代码")] 
        public const string Area = "COMMON:AREA";

        /// <summary>
        /// 字典下拉列表
        /// </summary>
        [Description("字典下拉列表")]
        public const string DictSelect = "COMMON:DICT:SELECT:{0}:{1}";

        /// <summary>
        /// 字典树
        /// </summary>
        [Description("字典树")]
        public const string DictTree = "COMMON:DICT:TREE:{0}:{1}";
    }
}
