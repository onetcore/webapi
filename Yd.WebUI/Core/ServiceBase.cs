using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Gentings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Yd.WebUI.Core
{
    /// <summary>
    /// 服务基类。
    /// </summary>
    public abstract class ServiceBase
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

        private User _user;
        /// <summary>
        /// 当前用户实例。
        /// </summary>
        protected User User => _user ??= GetRequiredService<User>();

        /// <summary>
        /// 客户端请求实例。
        /// </summary>
        protected HttpClient Client { get; }

        /// <summary>
        /// 初始化类<see cref="ServiceBase"/>。
        /// </summary>
        /// <param name="serviceProvider">服务提供者接口。</param>
        /// <param name="serviceName">服务名称，在注册<see cref="HttpClient"/>时候使用的名称。</param>
        protected ServiceBase(IServiceProvider serviceProvider, string serviceName = ServiceName)
        {
            _serviceProvider = serviceProvider;
            Client = serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient(serviceName);
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <returns>返回发送结果。</returns>
        protected virtual Task<ServiceDataResult<TResult>> GetAsync<TResult>(string api)
        {
            return CatchExecuteAsync(async () =>
            {
                var response = await Client.GetAsync(api);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Cores.FromJsonString<ServiceDataResult<TResult>>(result);
                }
                //如果不成功，则返回状态码
                return HandleFailuredAsync<ServiceDataResult<TResult>>(response.StatusCode);
            });
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns>返回发送结果。</returns>
        protected virtual async Task<TResult> GetDataAsync<TResult>(string api, TResult defaultValue = default)
        {
            var result = await GetAsync<TResult>(api);
            if (result.Status) return result.Data;
            return defaultValue;
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <returns>返回发送结果。</returns>
        protected virtual Task<ServicePageResult<TResult>> GetPageAsync<TResult>(string api)
        {
            return CatchExecuteAsync(async () =>
            {
                var response = await Client.GetAsync(api);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Cores.FromJsonString<ServicePageResult<TResult>>(result);
                }
                //如果不成功，则返回状态码
                return HandleFailuredAsync<ServicePageResult<TResult>>(response.StatusCode);
            });
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <param name="api">API地址。</param>
        /// <param name="data">数据实例。</param>
        /// <returns>返回发送结果。</returns>
        protected virtual Task<ServiceResult> PostAsync(string api, object data = null)
        {
            return CatchExecuteAsync(async () =>
            {
                var response = await Client.PostAsync(api, new StringContent(data.ToJsonString() ?? "{}"));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Cores.FromJsonString<ServiceResult>(result);
                }
                //如果不成功，则返回状态码
                return HandleFailuredAsync<ServiceResult>(response.StatusCode);
            });
        }

        /// <summary>
        /// 发送数据。
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型。</typeparam>
        /// <param name="api">API地址。</param>
        /// <param name="data">数据实例。</param>
        /// <returns>返回发送结果。</returns>
        protected virtual Task<ServiceDataResult<TResult>> PostAsync<TResult>(string api, object data = null)
        {
            return CatchExecuteAsync(async () =>
            {
                var response = await Client.PostAsync(api, new StringContent(data.ToJsonString() ?? "{}"));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Cores.FromJsonString<ServiceDataResult<TResult>>(result);
                }

                //如果不成功，则返回状态码
                return HandleFailuredAsync<ServiceDataResult<TResult>>(response.StatusCode);
            });
        }

        /// <summary>
        /// 捕获错误信息。
        /// </summary>
        /// <typeparam name="TResult">返回的结果实例。</typeparam>
        /// <param name="func">获取请求实例方法。</param>
        /// <returns>返回当前对象实例。</returns>
        protected virtual async Task<TResult> CatchExecuteAsync<TResult>(Func<Task<TResult>> func)
            where TResult : ServiceResult, new()
        {
            try
            {
                return await func();
            }
            catch (Exception exception)
            {
                return new TResult { Code = (int)HttpStatusCode.BadRequest, Message = exception.Message };
            }
        }

        /// <summary>
        /// 请求失败触发的事件实例。
        /// </summary>
        /// <typeparam name="TResult">返回当前结果。</typeparam>
        /// <param name="code">请求码。</param>
        /// <returns>返回请求失败结果。</returns>
        protected virtual TResult HandleFailuredAsync<TResult>(HttpStatusCode code)
            where TResult : ServiceResult, new()
        {
            if (code == HttpStatusCode.Unauthorized)
                HttpContext.Response.Redirect("/login");
            return new TResult
            {
                Code = (int)code,
                Status = false
            };
        }
    }
}