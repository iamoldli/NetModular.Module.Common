using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Common.Application.DictGroupService.ViewModels
{
    public class DictGroupAddModel
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [Required(ErrorMessage = "编码不能为空")]
        public string Code { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
    }
}
