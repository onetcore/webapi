using System;
using Gentings;
using Gentings.AspNetCore.AdminMenus;
using Gentings.Extensions.Emails;

namespace Yd.Extensions.RazorPages.Areas.Emails
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
        /// <param name="serviceProvider">服务提供者。</param>
        public AdminMenu(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 邮件管理。
        /// </summary>
        public const string Index = "emails.index";

        /// <summary>
        /// 邮件配置。
        /// </summary>
        public const string Settings = "emails.settings";

        /// <summary>
        /// 初始化菜单实例。
        /// </summary>
        /// <param name="root">根目录菜单。</param>
        public override void Init(MenuItem root)
        {
            if (_serviceProvider.IsServiceRegistered<IEmailManager>())
                root.AddMenu("emails", menu => menu.Texted("邮件管理", "mail").Page("/Admin/Index", area: EmailSettings.ExtensionName).Allow(EmailPermissions.Email)
                    .AddMenu("index",
                            it => it.Texted("邮件发送列表").Page("/Admin/Index", area: EmailSettings.ExtensionName))
                        .AddMenu("settings",
                            it => it.Texted("邮件配置").Page("/Admin/Settings/Index", area: EmailSettings.ExtensionName).Allow(EmailPermissions.Settings)));
        }
    }
}