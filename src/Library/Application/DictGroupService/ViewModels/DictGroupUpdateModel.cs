using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Common.Application.DictGroupService.ViewModels
{
    public class DictGroupUpdateModel : DictGroupAddModel
    {
        [Required(ErrorMessage = "请选择分组")]
        [Range(0, int.MaxValue, ErrorMessage = "数据不存在")]
        public int Id { get; set; }
    }
}
