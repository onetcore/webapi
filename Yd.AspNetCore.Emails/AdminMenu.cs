using Gentings.AspNetCore.AdminMenus;
using Gentings.Extensions.Emails;

namespace Yd.AspNetCore.Emails
{
    /// <summary>
    /// 管理菜单。
    /// </summary>
    public class AdminMenu : MenuProvider
    {
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
            root.AddMenu("emails", menu => menu.Texted("邮件管理", "fa-envelope-open-o").Page("/Admin/Index", area: EmailSettings.ExtensionName).Allow(EmailPermissions.Email)
                .AddMenu("index",
                        it => it.Texted("邮件发送列表").Page("/Admin/Index", area: EmailSettings.ExtensionName))
                    .AddMenu("settings",
                        it => it.Texted("邮件配置").Page("/Admin/Settings/Index", area: EmailSettings.ExtensionName).Allow(EmailPermissions.Settings)));
        }
    }
}