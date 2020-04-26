using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Module.Common.Infrastructure.DictSyncProvider
{
    /// <summary>
    /// 字典名称同步描述符
    /// </summary>
    public class DictSyncDescriptor
    {
        /// <summary>
        /// 分组编码
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public string DictCode { get; set; }

        /// <summary>
        /// 字典名称字段名
        /// </summary>
        public string DictNameColName { get; set; }

        /// <summary>
        /// 实体描述符
        /// </summary>
        public IEntityDescriptor EntityDescriptor { get; set; }

        /// <summary>
        /// 列描述
        /// </summary>
        public IColumnDescriptor ColumnDescriptor { get; set; }
    }
}
