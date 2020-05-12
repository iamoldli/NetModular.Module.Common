using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Common.Domain.DictItem;

namespace NetModular.Module.Common.Infrastructure.DictSyncProvider
{
    /// <summary>
    /// 默认字典名称同步提供器
    /// </summary>
    [Singleton]
    public class DefaultDictSyncProvider : IDictSyncProvider
    {
        private readonly DictSyncDescriptorCollection _descriptorCollection;
        private readonly ILogger<DefaultDictSyncProvider> _logger;

        public DefaultDictSyncProvider(DictSyncDescriptorCollection descriptorCollection, ILogger<DefaultDictSyncProvider> logger)
        {
            _descriptorCollection = descriptorCollection;
            _logger = logger;
        }

        public Task Sync(DictItemEntity entity, DictItemEntity oldEntity)
        {
            //如果名称或者值都未修改，则不需要同步
            if (entity.Name == oldEntity.Name && entity.Value == oldEntity.Value)
                return Task.CompletedTask;

            var descriptors = _descriptorCollection.Where(m => m.GroupCode == entity.GroupCode && m.DictCode == entity.DictCode).ToList();
            if (descriptors.Any())
            {
                var tasks = new List<Task>();
                foreach (var descriptor in descriptors)
                {
                    var sqlAdapter = descriptor.EntityDescriptor.SqlAdapter;

                    //构造参数
                    var dynParams = new DynamicParameters();

                    //新名称
                    var name = sqlAdapter.AppendParameter("name");
                    dynParams.Add(name, entity.Name);

                    //新值
                    var value = sqlAdapter.AppendParameter("val");
                    dynParams.Add(value, entity.Value);

                    //旧值
                    var oldValue = sqlAdapter.AppendParameter("oldVal");
                    dynParams.Add(oldValue, oldEntity.Value);

                    var tableName = sqlAdapter.AppendQuote(descriptor.EntityDescriptor.TableName);
                    var valColName = sqlAdapter.AppendQuote(descriptor.ColumnDescriptor.Name);
                    var nameColName = descriptor.DictNameColName.NotNull() ? descriptor.DictNameColName : descriptor.ColumnDescriptor.Name + "Name";
                    var sql = $"UPDATE {tableName} SET {sqlAdapter.AppendQuote(nameColName)}={name},{valColName}={value} WHERE {valColName}={oldValue};";

                    _logger.LogDebug(sql);

                    tasks.Add(descriptor.EntityDescriptor.DbSet.ExecuteAsync(sql, dynParams));
                }

                return Task.WhenAll(tasks);
            }

            return Task.CompletedTask;
        }
    }
}
