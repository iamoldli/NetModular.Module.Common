using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Common.Domain.DictGroup;
using NetModular.Module.Common.Domain.DictGroup.Models;

namespace NetModular.Module.Common.Infrastructure.Repositories.SqlServer
{
    public class DictGroupRepository : RepositoryAbstract<DictGroupEntity>, IDictGroupRepository
    {
        public DictGroupRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<DictGroupEntity>> Query(DictGroupQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));
            query.WhereNotNull(model.Code, m => m.Code == model.Code);

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);
            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y) => x.Id);
            }

            var list = await joinQuery.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<bool> ExistsCode(string code, int id = 0)
        {
            return Db.Find(m => m.Code == code).WhereIf(id > 0, m => m.Id != id).ExistsAsync();
        }
    }
}
