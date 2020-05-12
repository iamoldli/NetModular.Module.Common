using System;

namespace NetModular.Module.Common.Infrastructure.DictSyncProvider
{
    /// <summary>
    /// 字典同步特性
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
        /// 字典名称字段名
        /// </summary>
        public string DictNameColName { get; set; }

        /// <summary>
        /// 字典同步特性，该特性只能用于保存字典值的字段
        /// </summary>
        /// <param name="groupCode">字典分组编码</param>
        /// <param name="dictCode">字典编码</param>
        /// <param name="dictNameColName">保存字典名称的字段名，如果不设置，则采用字典值的字段名+Name后缀</param>
        public DictSyncAttribute(string groupCode, string dictCode, string dictNameColName = null)
        {
            GroupCode = groupCode;
            DictCode = dictCode;
            DictNameColName = dictNameColName;
        }
    }
}
