using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Common.Application.DictService.ViewModels
{
    /// <summary>
    /// 字典添加模型
    /// </summary>
    public class DictAddModel
    {
        /// <summary>
        /// 分组
        /// </summary>
        [Required(ErrorMessage = "请选择分组")]
        public string GroupCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "编码不能为空")]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "序号不能为空")]
        public int Sort { get; set; }
    }
}
