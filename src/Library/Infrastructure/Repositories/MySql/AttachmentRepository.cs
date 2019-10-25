using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Common.Infrastructure.Repositories.MySql
{
    public class AttachmentRepository : SqlServer.AttachmentRepository
    {
        public AttachmentRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}