using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Yd.Extensions.Controllers
{
    /// <summary>
    /// API扩展类。
    /// </summary>
    public static class ApiExtensions
    {
        /// <summary>
        /// 是否匿名可访问。
        /// </summary>
        /// <param name="descriptor">控制器描述实例。</param>
        /// <returns>返回判断结果。</returns>
        public static bool IsAnonymous(this ControllerActionDescriptor descriptor)
        {
            if (descriptor.EndpointMetadata.Any(x => x is AllowAnonymousAttribute))
                return true;
            return !descriptor.EndpointMetadata.Any(x => x is AuthorizeAttribute);
        }

        /// <summary>
        /// 是否匿名可访问。
        /// </summary>
        /// <param name="descriptor">操作描述实例。</param>
        /// <returns>返回判断结果。</returns>
        public static bool IsAnonymous(this ActionDescriptor descriptor)
        {
            if (descriptor.EndpointMetadata.Any(x => x is AllowAnonymousAttribute))
                return true;
            return !descriptor.EndpointMetadata.Any(x => x is AuthorizeAttribute);
        }
    }
}