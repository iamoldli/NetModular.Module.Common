using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Module.Abstractions;
using NetModular.Module.Common.Infrastructure.DictNoticeProvider;

namespace NetModular.Module.Common.Infrastructure
{
    public class ModuleServicesConfigurator : IModuleServicesConfigurator
    {
        public void Configure(IServiceCollection services, IModuleCollection modules, IHostEnvironment env)
        {
            //加载IDictItemListener的实现
            foreach (var module in modules)
            {
                LoadDictItemListeners(module.AssemblyDescriptor.Application, services);
                LoadDictItemListeners(module.AssemblyDescriptor.Infrastructure, services);
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
    }
}
