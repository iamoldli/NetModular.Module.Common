using System.Threading.Tasks;
using NetModular.Module.Common.Application.DictService.ViewModels;
using NetModular.Module.Common.Domain.Dict.Models;

namespace NetModular.Module.Common.Application.DictService
{
    /// <summary>
    /// 字典服务
    /// </summary>
    public interface IDictService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(DictQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(DictAddModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(int id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(int id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(DictUpdateModel model);

        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <param name="group"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<IResultModel> Select(string group, string code);

        /// <summary>
        /// 查询字典树
        /// </summary>
        /// <param name="group"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<IResultModel> Tree(string group, string code);
    }
}
