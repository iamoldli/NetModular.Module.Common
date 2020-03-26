using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Abstractions.SqlQueryable;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Common.Domain.Dict;
using NetModular.Module.Common.Domain.Dict.Models;
using NetModular.Module.Common.Domain.DictGroup;

namespace NetModular.Module.Common.Infrastructure.Repositories.SqlServer
{
    public class DictRepository : RepositoryAbstract<DictEntity>, IDictRepository
    {
        public DictRepository(IDbContext context) : base(context)
        {
        }

        private INetSqlQueryable BuildQuery(DictQueryModel model, out Paging paging)
        {
            paging = model.Paging();

            var query = Db.Find();
            query.WhereNotNull(model.GroupCode, m => m.GroupCode == model.GroupCode);
            query.WhereNotNull(model.Code, m => m.Code == model.Code);
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                   .LeftJoin<DictGroupEntity>((x, y, z) => x.GroupCode == z.Code)
                   .Select((x, y, z) => new { x, Creator = y.Name, GroupName = z.Name });

            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y, z) => x.Id);
            }

            return joinQuery;
        }

        public async Task<IList<DictEntity>> Query(DictQueryModel model)
        {
            var query = BuildQuery(model, out Paging paging);
            var list = await query.PaginationAsync<DictEntity>(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<IList<DictEntity>> QueryAll(DictQueryModel model)
        {
            var query = BuildQuery(model, out Paging _);
            return query.ToListAsync<DictEntity>();
        }

        public Task<bool> Exists(DictEntity entity)
        {
            var query = Db.Find(m => m.GroupCode == entity.GroupCode && m.Code == entity.Code);
            query.WhereIf(entity.Id > 0, m => m.Id != entity.Id);
            return query.ExistsAsync();
        }

        public Task<DictEntity> GetByCode(string group, string code)
        {
            return GetAsync(m => m.GroupCode == group && m.Code == code);
        }
    }
}
