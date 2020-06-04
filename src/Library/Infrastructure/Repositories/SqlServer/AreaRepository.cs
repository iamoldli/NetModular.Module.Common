using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Module.Common.Domain.Area;
using NetModular.Module.Common.Domain.Area.Models;

namespace NetModular.Module.Common.Infrastructure.Repositories.SqlServer
{
    public class AreaRepository : RepositoryAbstract<AreaEntity>, IAreaRepository
    {
        public AreaRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<AreaEntity>> Query(AreaQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find(m => m.ParentId == model.ParentId);
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));
            query.WhereNotNull(model.Code, m => m.Code.Contains(model.Code));

            var result = await query.PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }

        public Task<IList<AreaEntity>> QueryChildren(int parentId)
        {
            return Db.Find(m => m.ParentId == parentId).ToListAsync();
        }

        public Task<bool> Exists(AreaEntity entity)
        {
            var query = Db.Find(m => m.ParentId == entity.ParentId );
            query.Where(m => m.Name == entity.Name || m.Code == entity.Code);
            query.WhereIf(entity.Id > 0, m => m.Id != entity.Id);

            return query.ExistsAsync();
        }

        public Task<AreaEntity> GetByCode(string code)
        {
            return GetAsync(m => m.Code == code);
        }
    }
}
