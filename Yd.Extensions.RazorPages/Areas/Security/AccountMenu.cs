﻿using Gentings.AspNetCore.RazorPages.AdminMenus;
using Yd.Extensions;
using Yd.Extensions.Security;

namespace Yd.Extensions.RazorPages.Areas.Security
{
    /// <summary>
    /// 导航。
    /// </summary>
    public class AccountMenu : MenuProvider
    {
        /// <summary>
        /// 私有数据。
        /// </summary>
        public const string PersonalData = "personal.data";

        /// <summary>
        /// 修改密码。
        /// </summary>
        public const string ChangePassword = "change.password";

        /// <summary>
        /// 二次登录验证。
        /// </summary>
        public const string TwoFactorAuthentication = "two.factor";

        /// <summary>
        /// 社会化登录。
        /// </summary>
        public const string ExternalLogins = "external.logins";

        /// <summary>
        /// 首页。
        /// </summary>
        public const string Index = "account.index";

        /// <summary>
        /// 日志。
        /// </summary>
        public const string Log = "account.log";

        /// <summary>
        /// 更新头像。
        /// </summary>
        public const string Avatar = "account.avatar";

        /// <summary>
        /// 提供者名称，同一个名称归为同一个菜单。
        /// </summary>
        public override string Name => "account";

        /// <summary>
        /// 初始化菜单实例。
        /// </summary>
        /// <param name="root">根目录菜单。</param>
        public override void Init(MenuItem root)
        {
            root.AddMenu(Index, item => item.Texted("个人配置").Page("/Account/Index", area: SecuritySettings.ExtensionName))
                .AddMenu(ChangePassword, item => item.Texted("修改密码").Page("/Account/ChangePassword", area: SecuritySettings.ExtensionName))
                .AddMenu(Avatar, item => item.Texted("更新头像").Page("/Account/Avatar", area: SecuritySettings.ExtensionName))
                //.AddMenu(TwoFactorAuthentication, item => item.Texted("二次登录验证", "fa fa-mobile").Page("/Account/TwoFactorAuthentication", area: SecuritySettings.ExtensionName))
                //.AddMenu(ExternalLogins, item => item.Texted("社会化登录", "fa fa-gg").Page("/Account/ExternalLogins", area: SecuritySettings.ExtensionName))
                //.AddMenu(PersonalData, item => item.Texted("下载数据", "fa fa-download").Page("/Account/PersonalData", area: SecuritySettings.ExtensionName))
                .AddMenu(Log, item => item.Texted("活动日志").Page("/Account/Log", area: SecuritySettings.ExtensionName));
        }
    }
}