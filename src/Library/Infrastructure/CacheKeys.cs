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
        public const string AREA = "COMMON:AREA";

        /// <summary>
        /// 字典下拉列表
        /// </summary>
        [Description("字典下拉列表")]
        public const string DICT_SELECT = "COMMON:DICT:SELECT";

        /// <summary>
        /// 字典树
        /// </summary>
        [Description("字典树")]
        public const string DICT_TREE = "COMMON:DICT:TREE";
    }
}
