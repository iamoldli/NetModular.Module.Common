using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Common.Domain.DictGroup.Models;

namespace NetModular.Module.Common.Domain.DictGroup
{
    /// <summary>
    /// 字典分组仓储接口
    /// </summary>
    public interface IDictGroupRepository : IRepository<DictGroupEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<DictGroupEntity>> Query(DictGroupQueryModel model);

        /// <summary>
        /// 编码是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsCode(string code, int id = 0);
    }
}
