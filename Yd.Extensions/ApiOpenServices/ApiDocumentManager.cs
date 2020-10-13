using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Yd.Extensions.ApiOpenServices.OpenServices;

namespace Yd.Extensions.ApiOpenServices
{
    /// <summary>
    /// API文档管理类。
    /// </summary>
    public class ApiDocumentManager : DocumentManagerBase, IApiDocumentManager
    {
        /// <summary>
        /// 初始化类<see cref="ApiDocumentManager"/>。
        /// </summary>
        /// <param name="cache">缓存接口。</param>
        /// <param name="provider">Action描述实例提供者。</param>
        public ApiDocumentManager(IMemoryCache cache, IApiDescriptionGroupCollectionProvider provider) : base(cache, provider)
        {
        }

        /// <summary>
        /// 判断是否符合当前文档实例。
        /// </summary>
        /// <param name="descriptor">控制器操作实例。</param>
        /// <returns>返回判断结果。</returns>
        protected override bool IsValidated(ApiDescription descriptor)
        {
            if (base.IsValidated(descriptor))
                return !descriptor.ActionDescriptor.EndpointMetadata.Any(x => x is OpenServiceAttribute);
            return false;
        }
    }
}