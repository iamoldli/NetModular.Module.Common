using NetModular.Module.Common.Domain.DictItem;

namespace NetModular.Module.Common.Infrastructure.DictNoticeProvider
{
    /// <summary>
    /// 字典项变更通知处理器
    /// </summary>
    public interface IDictItemNoticeProvider
    {
        /// <summary>
        /// 变更通知
        /// </summary>
        /// <param name="entity">新实体</param>
        /// <param name="oldEntity">旧实体</param>
        void ChangeNotice(DictItemEntity entity, DictItemEntity oldEntity);

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="entity"></param>
        void DeleteNotice(DictItemEntity entity);
    }
}
