using  NetModular.Lib.Data.Query;

namespace  NetModular.Module.Common.Domain.Area.Models
{
    public class AreaQueryModel : QueryModel
    {
        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentId { get; set; }

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
