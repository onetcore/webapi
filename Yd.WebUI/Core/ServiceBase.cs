using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Gentings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Yd.WebUI.Security.Models;

namespace Yd.WebUI.Core
{
    /// <summary>
    /// 服务基类。
    /// </summary>
    public abstract class ServiceBase : IServiceBase
    {
        /// <summary>
        /// 客户端注册名称。
        /// </summary>
        public const string ServiceName = "apicore";

        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 获取服务接口实例。
        /// </summary>
        /// <typeparam name="TService">服务类型。</typeparam>
        /// <returns>返回当前服务实例对象。</returns>
        protected TService GetService<TService>() => _serviceProvider.GetService<TService>();

        /// <summary>
        /// 获取必须的服务接口实例。
        /// </summary>
        /// <typeparam name="TService">服务类型。</typeparam>
        /// <returns>返回当前服务实例对象。</returns>
        protected TService GetRequiredService<TService>() => _serviceProvider.GetRequiredService<TService>();

        /// <summary>
        /// HTTP上下文实例。
        /// </summary>
        protected HttpContext HttpContext => GetRequiredService<IHttpContextAccessor>().HttpContext;

        /// <summary>
        /// 当前登录用户。
        /// </summary>
        protected User User => GetRequiredService<User>();

        /// <summary>
        /// 客户端请求实例。
        /// </summary>
        public HttpClient Client { get; }

        /// <summary>
        /// 初始化类<see cref="ServiceBase"/>。
        /// </summary>
        /// <param name="serviceProvider">服务提供者接口。</param>
        protected ServiceBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Client = serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient(ServiceName);
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <param name="api">API地址。</param>
        /// <returns>返回发送结果。</returns>
        public virtual Task<string> GetAsync(string api)
        {
            return Client.GetStringAsync(api);
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <returns>返回发送结果。</returns>
        public virtual async Task<TResult> GetAsync<TResult>(string api)
        {
            var result = await GetAsync(api);
            if (string.IsNullOrEmpty(result))
                return default;
            return Cores.FromJsonString<TResult>(result);
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <returns>返回发送结果。</returns>
        public virtual async Task<ApiDataResult<TResult>> GetDataAsync<TResult>(string api)
        {
            var response = await Client.GetAsync(api);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Cores.FromJsonString<ApiDataResult<TResult>>(result);
            }
            //如果不成功，则返回状态码
            return OnFailured<ApiDataResult<TResult>>(response.StatusCode);
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <returns>返回发送结果。</returns>
        public virtual async Task<ApiPageResult<TResult>> GetPageAsync<TResult>(string api)
        {
            var response = await Client.GetAsync(api);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Cores.FromJsonString<ApiPageResult<TResult>>(result);
            }
            //如果不成功，则返回状态码
            return OnFailured<ApiPageResult<TResult>>(response.StatusCode);
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <param name="api">API地址。</param>
        /// <param name="data">数据实例。</param>
        /// <returns>返回发送结果。</returns>
        public virtual async Task<ApiResult> PostAsync(string api, object data = null)
        {
            var response = await Client.PostAsync(api, new StringContent(data.ToJsonString() ?? "{}"));
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Cores.FromJsonString<ApiResult>(result);
            }
            //如果不成功，则返回状态码
            return OnFailured<ApiResult>(response.StatusCode);
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <param name="data">数据实例。</param>
        /// <returns>返回发送结果。</returns>
        public virtual async Task<ApiDataResult<TResult>> PostAsync<TResult>(string api, object data = null)
        {
            var response = await Client.PostAsync(api, new StringContent(data.ToJsonString() ?? "{}"));
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Cores.FromJsonString<ApiDataResult<TResult>>(result);
            }
            //如果不成功，则返回状态码
            return OnFailured<ApiDataResult<TResult>>(response.StatusCode);
        }

        /// <summary>
        /// 请求失败触发的事件实例。
        /// </summary>
        /// <typeparam name="TResult">返回当前结果。</typeparam>
        /// <param name="code">请求码。</param>
        /// <returns>返回请求失败结果。</returns>
        protected virtual TResult OnFailured<TResult>(HttpStatusCode code)
            where TResult : ApiResult,new()
        {
            if(code == HttpStatusCode.Unauthorized)
                HttpContext.Response.Redirect("/login");
            return new TResult
            {
                Code = (int)code,
                Status = false
            };
        }
    }
}