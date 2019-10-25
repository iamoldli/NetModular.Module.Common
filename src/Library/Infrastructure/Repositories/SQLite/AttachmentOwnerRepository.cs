using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Common.Infrastructure.Repositories.SQLite
{
    public class AttachmentOwnerRepository : SqlServer.AttachmentOwnerRepository
    {
        public AttachmentOwnerRepository(IDbContext context) : base(context)
        {
        }
    }
}
