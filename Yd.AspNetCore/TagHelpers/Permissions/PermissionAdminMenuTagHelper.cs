using System;
using Gentings.AspNetCore.AdminMenus;
using Gentings.AspNetCore.AdminMenus.TagHelpers;
using Gentings.Security.Permissions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace Yd.AspNetCore.TagHelpers.Permissions
{
    /// <summary>
    /// 管理员菜单标签。
    /// </summary>
    [HtmlTargetElement("gt:permission-menu", Attributes = "provider")]
    public class PermissionAdminMenuTagHelper : AdminMenuTagHelper
    {
        private readonly IPermissionManager _permissionManager;
        /// <summary>
        /// 初始化类<see cref="PermissionAdminMenuTagHelper"/>。
        /// </summary>
        /// <param name="menuProviderFactory">菜单提供者工厂接口。</param>
        /// <param name="factory">URL辅助类工厂接口。</param>
        /// <param name="serviceProvider">服务提供者。</param>
        public PermissionAdminMenuTagHelper(IMenuProviderFactory menuProviderFactory, IUrlHelperFactory factory, IServiceProvider serviceProvider) 
            : base(menuProviderFactory, factory)
        {
            _permissionManager = serviceProvider.GetService<IPermissionManager>();
        }

        /// <summary>
        /// 判断是否具有权限。
        /// </summary>
        /// <param name="item">菜单项。</param>
        /// <returns>返回验证结果。</returns>
        public override bool IsAuthorized(MenuItem item)
        {
            if (item.PermissionName == null || _permissionManager == null)
                return true;
            return _permissionManager.IsAuthorized(item.PermissionName);
        }
    }
}