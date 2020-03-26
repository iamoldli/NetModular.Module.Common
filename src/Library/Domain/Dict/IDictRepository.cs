using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Common.Domain.Dict.Models;

namespace NetModular.Module.Common.Domain.Dict
{
    /// <summary>
    /// 字典仓储
    /// </summary>
    public interface IDictRepository : IRepository<DictEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<DictEntity>> Query(DictQueryModel model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<DictEntity>> QueryAll(DictQueryModel model);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        Task<bool> Exists(DictEntity entity);

        /// <summary>
        /// 根据编码查询
        /// </summary>
        /// <param name="group"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<DictEntity> GetByCode(string group, string code);
    }
}
