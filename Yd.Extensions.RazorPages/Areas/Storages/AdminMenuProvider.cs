using System;
using Gentings.AspNetCore.RazorPages.AdminMenus;
using Yd.Extensions;

namespace Yd.Extensions.RazorPages.Areas.Storages
{
    /// <summary>
    /// 管理菜单。
    /// </summary>
    public class AdminMenuProvider : MenuProvider
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 初始化类<see cref="Yd.Extensions.RazorPages.Areas.Core.AdminMenuProvider"/>。
        /// </summary>
        /// <param name="serviceProvider">服务提供者接口。</param>
        public AdminMenuProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 区域名称。
        /// </summary>
        public const string AreaName = "Storages";

        /// <summary>
        /// 初始化菜单实例。
        /// </summary>
        /// <param name="root">根目录菜单。</param>
        public override void Init(MenuItem root)
        {
            root.AddMenu("sys", menu => menu
                .AddMenu("storages", it => it.Texted("文件管理").Page("/Admin/Index", area: AreaName).Allow(Permissions.Storages))
            );
        }
    }
}
