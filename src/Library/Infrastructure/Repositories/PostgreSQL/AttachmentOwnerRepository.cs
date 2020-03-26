using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Common.Infrastructure.Repositories.PostgreSQL
{
    public class AttachmentOwnerRepository : SqlServer.AttachmentOwnerRepository
    {
        public AttachmentOwnerRepository(IDbContext context) : base(context)
        {
        }
    }
}
