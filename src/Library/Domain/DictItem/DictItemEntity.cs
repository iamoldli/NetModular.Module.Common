using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;

namespace NetModular.Module.Common.Domain.DictItem
{
    /// <summary>
    /// 字典项
    /// </summary>
    [Table("Dict_Item")]
    public class DictItemEntity : EntityBase<int>
    {
        /// <summary>
        /// 分组编号
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// 字典编号
        /// </summary>
        public string DictCode { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Length(100)]
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Length(100)]
        public string Value { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        [Nullable]
        [Length(400)]
        public string Extend { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
    }
}
