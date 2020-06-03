using System.ComponentModel;
using NetModular.Lib.Quartz.Abstractions;
using System.Threading.Tasks;
using NetModular.Module.Common.Application.AreaService;
using NetModular.Module.Common.Infrastructure.AreaCrawlingHandler;

namespace NetModular.Module.Common.Quartz
{
    [Description("区划代码爬取服务")]
    public class AreaCrawlingTask : TaskAbstract
    {
        private readonly IAreaCrawlingHandler _crawlingHandler;
        private readonly IAreaService _service;

        public AreaCrawlingTask(ITaskLogger logger, IAreaCrawlingHandler crawlingHandler, IAreaService service) : base(logger)
        {
            _crawlingHandler = crawlingHandler;
            _service = service;
        }

        public override async Task Execute(ITaskExecutionContext context)
        {
            var list = await _crawlingHandler.Crawling();
            await _service.CrawlInsert(list);
        }
    }
}
