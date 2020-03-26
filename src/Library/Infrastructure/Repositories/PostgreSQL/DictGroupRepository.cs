using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Common.Infrastructure.Repositories.PostgreSQL
{
    public class DictGroupRepository : SqlServer.DictGroupRepository
    {
        public DictGroupRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}