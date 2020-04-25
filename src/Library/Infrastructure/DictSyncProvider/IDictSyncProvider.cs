using System.Threading.Tasks;
using NetModular.Module.Common.Domain.DictItem;

namespace NetModular.Module.Common.Infrastructure.DictSyncProvider
{
    /// <summary>
    /// 字典名称同步处理器
    /// </summary>
    public interface IDictSyncProvider
    {
        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="entity">新字典</param>
        /// <param name="oldEntity">旧字典</param>
        /// <returns></returns>
        Task Sync(DictItemEntity entity, DictItemEntity oldEntity);
    }
}
