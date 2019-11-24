using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Common.Application.DictService.ViewModels
{
    /// <summary>
    /// 字典添加模型
    /// </summary>
    public class DictUpdateModel : DictAddModel
    {
        [Required(ErrorMessage = "请选择要修改的字典")]
        public int Id { get; set; }
    }
}
