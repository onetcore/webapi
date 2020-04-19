using System.Threading.Tasks;

namespace Yd.WebUI.Core
{
    /// <summary>
    /// 服务接口基类。
    /// </summary>
    public interface IServiceBase
    {
        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <param name="api">API地址。</param>
        /// <returns>返回发送结果。</returns>
        Task<string> GetAsync(string api);

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <returns>返回发送结果。</returns>
        Task<TResult> GetAsync<TResult>(string api);

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <returns>返回发送结果。</returns>
        Task<ApiDataResult<TResult>> GetDataAsync<TResult>(string api);

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <returns>返回发送结果。</returns>
        Task<ApiPageResult<TResult>> GetPageAsync<TResult>(string api);

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <param name="api">API地址。</param>
        /// <param name="data">数据实例。</param>
        /// <returns>返回发送结果。</returns>
        Task<ApiResult> PostAsync(string api, object data = null);

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <param name="data">数据实例。</param>
        /// <returns>返回发送结果。</returns>
        Task<ApiDataResult<TResult>> PostAsync<TResult>(string api, object data = null);
    }
}