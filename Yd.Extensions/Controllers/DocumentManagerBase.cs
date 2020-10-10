using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Gentings.Extensions;
using Yd.Extensions.Controllers.Documents;

namespace Yd.Extensions.Controllers
{
    /// <summary>
    /// 接口管理实现基类。
    /// </summary>
    public abstract class DocumentManagerBase : IDocumentManagerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IActionDescriptorCollectionProvider _provider;
        /// <summary>
        /// 初始化类<see cref="DocumentManagerBase"/>。
        /// </summary>
        /// <param name="cache">缓存接口。</param>
        /// <param name="provider">Action描述实例提供者。</param>
        protected DocumentManagerBase(IMemoryCache cache, IActionDescriptorCollectionProvider provider)
        {
            _cache = cache;
            _provider = provider;
        }

        /// <summary>
        /// 获取所有API描述。
        /// </summary>
        /// <returns>返回API描述列表。</returns>
        public virtual IEnumerable<ApiDescriptor> GetApiDescriptors()
        {
            return _cache.GetOrCreate(typeof(ApiDescriptor), ctx =>
            {
                ctx.SetDefaultAbsoluteExpiration();
                return _provider.ActionDescriptors.Items
                    .Select(x => x as ControllerActionDescriptor)
                    .Where(x => x != null)
                    .Where(IsValidated)
                    .Select(x => new ApiDescriptor
                    {
                        GroupName = x.ControllerTypeInfo.GetCustomAttribute<ApiServiceAttribute>()?.GroupName ?? "core",
                        ControllerName = x.ControllerName,
                        Assembly = new AssemblyInfo(x.ControllerTypeInfo.Assembly),
                        ActionName = x.ActionName,
                        DisplayName = x.DisplayName,
                        RouteTemplate = x.AttributeRouteInfo.Template.ToLower(),
                        HttpMethod = x.MethodInfo.GetHttpMethod(),
                        RouteValues = x.RouteValues,
                        Parameters = x.Parameters,
                        Summary = x.MethodInfo.GetSummary(),
                        IsAnonymous = x.IsAnonymous(),
                    })
                    .OrderBy(x => x.Assembly.AssemblyName)
                    .ThenBy(x => x.ControllerName)
                    .ToList();
            });
        }

        /// <summary>
        /// 判断是否符合当前文档实例。
        /// </summary>
        /// <param name="descriptor">控制器操作实例。</param>
        /// <returns>返回判断结果。</returns>
        protected virtual bool IsValidated(ControllerActionDescriptor descriptor)
        {
            var settings = descriptor.ControllerTypeInfo.GetCustomAttribute<ApiServiceAttribute>();
            return settings?.IgnoreApi != true;
        }

        /// <summary>
        /// 获取程序集名称。
        /// </summary>
        /// <returns>程序集名称列表。</returns>
        public virtual IEnumerable<AssemblyInfo> GetAssemblies()
        {
            return GetApiDescriptors().Select(x => x.Assembly).Distinct().OrderBy(x => x);
        }

        /// <summary>
        /// 分组获取API描述实例。
        /// </summary>
        /// <returns>返回API描述实例字典列表。</returns>
        public virtual IDictionary<string, IEnumerable<ApiDescriptor>> GetGroupApiDescriptors()
        {
            return GetApiDescriptors()
                .GroupBy(x => x.GroupName, x => x, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(x => x.Key, x => x.AsEnumerable());
        }
    }
}