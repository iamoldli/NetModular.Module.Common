using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Common.Domain.DictItem.Models;

namespace NetModular.Module.Common.Domain.DictItem
{
    public interface IDictItemRepository : IRepository<DictItemEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<DictItemEntity>> Query(DictItemQueryModel model);

        /// <summary>
        /// 查询指定字典的所有数据项
        /// </summary>
        /// <param name="groupCode"></param>
        /// <param name="dictCode"></param>
        /// <returns></returns>
        Task<IList<DictItemEntity>> QueryAll(string groupCode, string dictCode);

        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> ExistsName(DictItemEntity entity);

        /// <summary>
        /// 值是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> ExistsValue(DictItemEntity entity);

        /// <summary>
        /// 是否存在指定字典的项
        /// </summary>
        /// <param name="groupCode"></param>
        /// <param name="dictCode"></param>
        /// <returns></returns>
        Task<bool> ExistsDict(string groupCode, string dictCode);

        /// <summary>
        /// 查询子节点
        /// </summary>
        /// <param name="group"></param>
        /// <param name="code"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<IList<DictItemEntity>> QueryChildren(string group, string code, int parentId = 0);

        /// <summary>
        /// 是否存在子节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsChild(int id);
    }
}
