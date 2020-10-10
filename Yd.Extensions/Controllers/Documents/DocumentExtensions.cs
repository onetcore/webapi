using System;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.Controllers.Documents
{
    /// <summary>
    /// 扩展方法类型。
    /// </summary>
    public static class DocumentExtensions
    {
        /// <summary>
        /// 获取HTTP请求方法。
        /// </summary>
        /// <param name="info">当前方法实例。</param>
        /// <returns>返回HTTP请求方法。</returns>
        public static HttpMethod GetHttpMethod(this MemberInfo info)
        {
            if (info.IsDefined(typeof(HttpPostAttribute)))
                return HttpMethod.Post;
            if (info.IsDefined(typeof(HttpPutAttribute)))
                return HttpMethod.Put;
            if (info.IsDefined(typeof(HttpDeleteAttribute)))
                return HttpMethod.Delete;
            if (info.IsDefined(typeof(HttpHeadAttribute)))
                return HttpMethod.Head;
            if (info.IsDefined(typeof(HttpPatchAttribute)))
                return HttpMethod.Patch;
            if (info.IsDefined(typeof(HttpOptionsAttribute)))
                return HttpMethod.Options;
            return HttpMethod.Get;
        }

        /// <summary>
        /// 获取方法注释。
        /// </summary>
        /// <param name="info">当前方法实例。</param>
        /// <returns>返回方法注释实例。</returns>
        public static MethodDescriptor GetSummary(this MemberInfo info)
        {
            var typeDescriptor = AssemblyDocument.GetTypeDescriptor(info.DeclaringType);
            return typeDescriptor?.GetMethodDescriptor(info);
        }

        /// <summary>
        /// 获取HTTP请求方法颜色。
        /// </summary>
        /// <param name="method">HTTP请求方法。</param>
        /// <returns>返回HTTP请求方法颜色。</returns>
        public static string GetColor(this HttpMethod method)
        {
            if (method == HttpMethod.Post)
                return "#28a745";
            if (method == HttpMethod.Get)
                return "#007bff";
            if (method == HttpMethod.Delete)
                return "#dc3545";
            if (method == HttpMethod.Put)
                return "#d39e00";
            if (method == HttpMethod.Head)
                return "#0062cc";
            if (method == HttpMethod.Patch)
                return "#17a2b8";
            if (method == HttpMethod.Options)
                return "#adb5bd";
            return "#007bff";
        }

        /// <summary>
        /// 是否可以链接。
        /// </summary>
        /// <param name="type">类型实例。</param>
        /// <returns>返回判断结果。</returns>
        public static bool IsLinkable(this Type type)
        {
            return AssemblyDocument.GetTypeDescriptor(type) != null;
        }
    }
}