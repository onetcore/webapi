using System;
using Gentings.AspNetCore.AdminMenus;
using Gentings.Extensions.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Yd.Extensions.RazorPages.Areas.Core
{
    /// <summary>
    /// 管理菜单。
    /// </summary>
    public class AdminMenu : MenuProvider
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 初始化类<see cref="AdminMenu"/>。
        /// </summary>
        /// <param name="serviceProvider">服务提供者接口。</param>
        public AdminMenu(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 区域名称。
        /// </summary>
        public const string AreaName = "Core";

        /// <summary>
        /// 初始化菜单实例。
        /// </summary>
        /// <param name="root">根目录菜单。</param>
        public override void Init(MenuItem root)
        {
            root.AddMenu("sys", menu =>
            {
                menu.Texted("系统管理", "fa-cogs");
                menu.AddMenu("notifier",
                    it => it.Texted("通知类型").Page("/Admin/Notifications/Index", area: AreaName)
                        .Allow(CorePermissions.Notifier));
                menu.AddMenu("sensitive",
                    it => it.Texted("敏感词汇").Page("/Admin/SensitiveWords/Index", area: AreaName)
                        .Allow(CorePermissions.Sensitive));
                if (_serviceProvider.GetService<ISettingDictionaryManager>() != null)
                    menu.AddMenu("dicsettings",
                        it => it.Texted("字典管理").Page("/Admin/DictionarySettings/Index", area: AreaName)
                            .Allow(CorePermissions.DictionarySettings));
                menu.AddMenu("settings",
                        it => it.Texted("系统配置").Page("/Admin/Settings", area: AreaName)
                    .Allow(CorePermissions.SiteSettings));
                menu.AddMenu("tasks",
                        it => it.Texted("后台任务管理").Page("/Admin/Tasks/Index", area: AreaName)
                            .Allow(CorePermissions.Task))
                    .AddMenu("background-service",
                        it => it.Texted("后台服务列表").Page("/Admin/Tasks/BackgroundService", area: AreaName)
                            .Allow(CorePermissions.Task));
            });
        }
    }
}
