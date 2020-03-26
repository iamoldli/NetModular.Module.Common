using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Common.Infrastructure.Repositories.PostgreSQL
{
    public class AttachmentRepository : SqlServer.AttachmentRepository
    {
        public AttachmentRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}