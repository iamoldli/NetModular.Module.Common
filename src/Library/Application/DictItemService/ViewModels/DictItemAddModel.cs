using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Common.Application.DictItemService.ViewModels
{
    public class DictItemAddModel
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
        [Required(ErrorMessage = "请输入名称")]
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "请输入值")]
        public string Value { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public string Extend { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
