using System;

namespace NetModular.Module.Common.Infrastructure.DictSyncProvider
{
    /// <summary>
    /// 字典同步
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DictSyncAttribute : Attribute
    {
        /// <summary>
        /// 分组编码
        /// </summary>
        public string GroupCode { get; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public string DictCode { get; }

        /// <summary>
        /// 字典同步
        /// </summary>
        /// <param name="groupCode">字典分组编码</param>
        /// <param name="dictCode">字典编码</param>
        public DictSyncAttribute(string groupCode, string dictCode)
        {
            GroupCode = groupCode;
            DictCode = dictCode;
        }
    }
}
