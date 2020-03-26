using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Common.Infrastructure.Repositories.PostgreSQL
{
    public class DictRepository : SqlServer.DictRepository
    {
        public DictRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}