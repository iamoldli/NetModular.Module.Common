using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Module.Abstractions;
using NetModular.Module.Common.Infrastructure.DictNoticeProvider;
using NetModular.Module.Common.Infrastructure.DictSyncProvider;

namespace NetModular.Module.Common.Infrastructure
{
    public class ModuleServicesConfigurator : IModuleServicesConfigurator
    {
        public void Configure(IServiceCollection services, IModuleCollection modules, IHostEnvironment env, IConfiguration cfg)
        {
            foreach (var module in modules)
            {
                //加载IDictItemListener的实现
                LoadDictItemListeners(module.AssemblyDescriptor.Application, services);
                LoadDictItemListeners(module.AssemblyDescriptor.Infrastructure, services);

                //加载字典名称同步描述符
                LoadDictNameSyncDescriptors(services, module);
            }
        }

        private void LoadDictItemListeners(Assembly assembly, IServiceCollection services)
        {
            var types = assembly.GetTypes().Where(m => typeof(IDictItemListener).IsAssignableFrom(m) && m != typeof(IDictItemListener));
            if (types.Any())
            {
                foreach (var type in types)
                {
                    services.AddSingleton(typeof(IDictItemListener), type);
                }
            }
        }

        private void LoadDictNameSyncDescriptors(IServiceCollection services, IModuleDescriptor moduleDescriptor)
        {
            var collection = new DictSyncDescriptorCollection();

            //因为EntityDescriptorCollection未提供获取所有实体描述符的方法，所以暂时只能通过模块查询
            var entityDescriptors = EntityDescriptorCollection.Get(moduleDescriptor.Code);
            foreach (var entityDescriptor in entityDescriptors)
            {
                foreach (var column in entityDescriptor.Columns)
                {
                    var attr = (DictSyncAttribute)Attribute.GetCustomAttribute(column.PropertyInfo, typeof(DictSyncAttribute));
                    if (attr != null)
                    {
                        collection.Add(new DictSyncDescriptor
                        {
                            GroupCode = attr.GroupCode,
                            DictCode = attr.DictCode,
                            DictNameColName = attr.DictNameColName,
                            EntityDescriptor = entityDescriptor,
                            ColumnDescriptor = column
                        });
                    }
                }
            }

            services.AddSingleton(collection);
        }
    }
}
