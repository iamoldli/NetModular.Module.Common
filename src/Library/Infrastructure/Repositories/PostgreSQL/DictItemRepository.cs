using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Common.Infrastructure.Repositories.PostgreSQL
{
    public class DictItemRepository : SqlServer.DictItemRepository
    {
        public DictItemRepository(IDbContext context) : base(context)
        {
        }
    }
}
