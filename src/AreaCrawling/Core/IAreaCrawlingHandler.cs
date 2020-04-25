using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Module.Common.Application.AreaService.ViewModels;

namespace NetModular.Module.Common.AreaCrawling.Core
{
    /// <summary>
    /// 行政区域数据爬取处理
    /// </summary>
    public interface IAreaCrawlingHandler
    {
        Task<IList<AreaCrawlingModel>> Crawling();
    }
}
