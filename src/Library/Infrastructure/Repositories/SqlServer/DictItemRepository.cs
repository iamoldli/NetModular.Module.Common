using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Common.Domain.DictItem;
using NetModular.Module.Common.Domain.DictItem.Models;

namespace NetModular.Module.Common.Infrastructure.Repositories.SqlServer
{
    public class DictItemRepository : RepositoryAbstract<DictItemEntity>, IDictItemRepository
    {
        public DictItemRepository(IDbContext context) : base(context)
        {

        }

        public async Task<IList<DictItemEntity>> Query(DictItemQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find(m => m.GroupCode == model.GroupCode && m.DictCode == model.DictCode && m.ParentId == model.ParentId);
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));
            query.WhereNotNull(model.Value, m => m.Value.Contains(model.Value));

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name });

            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderBy((x, y) => x.Sort);
            }

            var list = await joinQuery.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<IList<DictItemEntity>> QueryAll(string groupCode, string dictCode)
        {
            var query = Db.Find(m => m.GroupCode == groupCode && m.DictCode == dictCode);
            query.OrderBy(m => m.Sort);
            return query.ToListAsync();
        }

        public Task<bool> ExistsName(DictItemEntity entity)
        {
            return Db.Find(m => m.GroupCode == entity.GroupCode && m.DictCode == entity.DictCode && m.Name == entity.Name && m.ParentId == entity.ParentId)
                .WhereNotNull(entity.Id > 0, m => m.Id != entity.Id).ExistsAsync();
        }

        public Task<bool> ExistsValue(DictItemEntity entity)
        {
            return Db.Find(m => m.GroupCode == entity.GroupCode && m.DictCode == entity.DictCode && m.Value == entity.Value && m.ParentId == entity.ParentId)
                .WhereNotNull(entity.Id > 0, m => m.Id != entity.Id).ExistsAsync();
        }

        public Task<bool> ExistsDict(string groupCode, string dictCode)
        {
            return Db.Find(m => m.GroupCode == groupCode && m.DictCode == dictCode).ExistsAsync();
        }

        public Task<IList<DictItemEntity>> QueryChildren(string group, string code, int parentId = 0)
        {
            if (parentId < 0)
                parentId = 0;

            return Db.Find(m => m.GroupCode == group && m.DictCode == code && m.ParentId == parentId)
                .OrderBy(m => m.Sort).ToListAsync();
        }

        public Task<bool> ExistsChild(int id)
        {
            return Db.Find(m => m.ParentId == id).ExistsAsync();
        }
    }
}
