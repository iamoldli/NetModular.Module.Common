using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Common.Domain.AttachmentOwner;

namespace NetModular.Module.Common.Infrastructure.Repositories.SqlServer
{
    public class AttachmentOwnerRepository : RepositoryAbstract<AttachmentOwnerEntity>, IAttachmentOwnerRepository
    {
        public AttachmentOwnerRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> Exist(AttachmentOwnerEntity entity)
        {
            return Db.Find(m => m.AccountId == entity.AccountId && m.AttachmentId == entity.AttachmentId).ExistsAsync();
        }
    }
}
