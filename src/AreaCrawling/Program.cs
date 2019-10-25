using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Host.Generic;
using NetModular.Module.Common.Infrastructure.AreaCrawling;
using IHostEnvironment = Microsoft.Extensions.Hosting.IHostEnvironment;

namespace NetModular.Module.Common.AreaCrawling
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new HostBuilder().Run<Startup>(args, ConfigureServices);
        }

        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        static void ConfigureServices(IServiceCollection services, IHostEnvironment env)
        {
            services.AddSingleton<IAreaCrawlingHandler, AreaCrawlingHandler>();
        }
    }
}
