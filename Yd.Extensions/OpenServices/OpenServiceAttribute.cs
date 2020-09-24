using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Yd.Extensions.OpenServices
{
    /// <summary>
    /// 开放服务特性。
    /// </summary>
    public class OpenServiceAttribute : ApiControllerAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 在管道中确认当前请求是否已经通过验证。
        /// </summary>
        /// <param name="context">验证过滤器上下文<see cref="T:Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext" />实例。</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {

        }
    }
}