using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Common.Infrastructure.Repositories.SQLite
{
    public class AttachmentRepository : SqlServer.AttachmentRepository
    {
        public AttachmentRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
