using NetModular.Lib.Data.Query;

namespace NetModular.Module.Common.Domain.DictGroup.Models
{
    public class DictGroupQueryModel : QueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}
