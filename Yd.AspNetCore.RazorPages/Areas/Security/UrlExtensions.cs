using Microsoft.AspNetCore.Mvc;
using Yd.Extensions;

namespace Yd.AspNetCore.RazorPages.Areas.Security
{
    /// <summary>
    /// URL扩展类。
    /// </summary>
    internal static class UrlExtensions
    {
        public static string GetDirection(this IUrlHelper helper, LoginDirection direction)
        {
            switch (direction)
            {
                case LoginDirection.Account:
                    return helper.Page("/Account/Index");
                case LoginDirection.Admin:
                    return helper.Page("/Admin/Index");
                default:
                    return helper.Page("/Index");
            }
        }
    }
}