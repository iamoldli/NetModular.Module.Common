using System.Threading.Tasks;
using NetModular.Module.Common.Domain.DictItem;

namespace NetModular.Module.Common.Infrastructure.DictNoticeProvider
{
    /// <summary>
    /// 字典项监听器
    /// </summary>
    public interface IDictItemListener
    {
        /// <summary>
        /// 分组编码
        /// </summary>
        string GroupCode { get; }

        /// <summary>
        /// 字典编码
        /// </summary>
        string DictCode { get; }

        /// <summary>
        /// 变更事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="oldEntity"></param>
        /// <returns></returns>
        Task OnChange(DictItemEntity entity, DictItemEntity oldEntity);

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task OnDelete(DictItemEntity entity);
    }
}
