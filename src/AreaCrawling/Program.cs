using System.Threading.Tasks;
using NetModular.Lib.Host.Generic;

namespace NetModular.Module.Common.AreaCrawling
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new HostBuilder().Run<Startup>(args);
        }
    }
}
