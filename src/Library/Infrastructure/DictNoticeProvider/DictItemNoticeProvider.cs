using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Common.Domain.DictItem;

namespace NetModular.Module.Common.Infrastructure.DictNoticeProvider
{
    [Singleton]
    public class DictItemNoticeProvider : IDictItemNoticeProvider
    {
        private readonly IServiceProvider _sp;

        public DictItemNoticeProvider(IServiceProvider sp)
        {
            _sp = sp;
        }

        public void ChangeNotice(DictItemEntity entity, DictItemEntity oldEntity)
        {
            var list = _sp.GetServices<IDictItemListener>()
                .Where(m => m.GroupCode.EqualsIgnoreCase(entity.GroupCode) && m.DictCode.EqualsIgnoreCase(entity.DictCode)).ToList();

            if (list.Any())
            {
                var tasks = new List<Task>();
                foreach (var listener in list)
                {
                    tasks.Add(listener.OnChange(entity, oldEntity));
                }

                Task.WaitAll(tasks.ToArray());
            }
        }

        public void DeleteNotice(DictItemEntity entity)
        {
            var list = _sp.GetServices<IDictItemListener>()
                .Where(m => m.GroupCode.EqualsIgnoreCase(entity.GroupCode) && m.DictCode.EqualsIgnoreCase(entity.DictCode)).ToList();

            if (list.Any())
            {
                var tasks = new List<Task>();
                foreach (var listener in list)
                {
                    tasks.Add(listener.OnDelete(entity));
                }

                Task.WaitAll(tasks.ToArray());
            }
        }
    }
}
