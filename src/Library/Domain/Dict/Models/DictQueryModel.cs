using NetModular.Lib.Data.Query;

namespace NetModular.Module.Common.Domain.Dict.Models
{
    public class DictQueryModel : QueryModel
    {
        /// <summary>
        /// 分组编码
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// 名称，模糊查询
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}
