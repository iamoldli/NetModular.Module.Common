using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetModular.Module.Common.Application.AreaService;
using NetModular.Module.Common.Infrastructure.AreaCrawling;

namespace NetModular.Module.Common.AreaCrawling
{
    public class Startup : IHostedService, IDisposable
    {
        private readonly IAreaCrawlingHandler _crawlingHandler;
        private readonly IAreaService _service;
        private readonly ILogger _logger;

        public Startup(IAreaService service, ILogger<Startup> logger, IAreaCrawlingHandler crawlingHandler)
        {
            _service = service;
            _logger = logger;
            _crawlingHandler = crawlingHandler;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("开始爬取区域代码数据");

                var list = await _crawlingHandler.Crawling();
                await _service.CrawlInsert(list);

                _logger.LogInformation("爬取结束");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据同步失败");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
