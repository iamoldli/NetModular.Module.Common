using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Common.Domain.AttachmentOwner
{
    public interface IAttachmentOwnerRepository : IRepository<AttachmentOwnerEntity>
    {
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exist(AttachmentOwnerEntity entity);
    }
}
