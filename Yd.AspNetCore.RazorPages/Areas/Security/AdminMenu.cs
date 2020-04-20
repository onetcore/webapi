using Gentings.AspNetCore.RazorPages.AdminMenus;
using Yd.Extensions;

namespace Yd.AspNetCore.RazorPages.Areas.Security
{
    /// <summary>
    /// 管理菜单。
    /// </summary>
    public class AdminMenu : MenuProvider
    {
        /// <summary>
        /// 初始化菜单实例。
        /// </summary>
        /// <param name="root">根目录菜单。</param>
        public override void Init(MenuItem root)
        {
            root.AddMenu("users", item => item.Texted("用户管理", "fa-users").Page("/Admin/Index", area: SecuritySettings.ExtensionName)
                .AddMenu("index", it => it.Texted("用户列表").Page("/Admin/Index", area: SecuritySettings.ExtensionName).Allow(Permissions.Users))
                .AddMenu("roles", it => it.Texted("角色列表").Page("/Admin/Roles/Index", area: SecuritySettings.ExtensionName).Allow(Permissions.Roles))
                .AddMenu("permissions", it => it.Texted("权限列表").Page("/Admin/Permissions/Index", area: SecuritySettings.ExtensionName).Allow(Permissions.PermissionManager))
                .AddMenu("logs", it => it.Texted("日志管理").Page("/Admin/Logs/Index", area: SecuritySettings.ExtensionName).Allow(Permissions.Logs))
                .AddMenu("settings", it => it.Texted("用户配置").Page("/Admin/Settings", area: SecuritySettings.ExtensionName).Allow(Permissions.Settings))
            );
            root.AddMenu("account", item => item.Texted("账号管理", "fa-user").Page("/Admin/User/Index", area: SecuritySettings.ExtensionName)
                .AddMenu("index", it => it.Texted("编辑资料").Page("/Admin/User/Index", area: SecuritySettings.ExtensionName))
                .AddMenu("changepassword", it => it.Texted("修改密码").Page("/Admin/User/ChangePassword", area: SecuritySettings.ExtensionName))
                .AddMenu("avatar", it => it.Texted("更新头像").Page("/Admin/User/Avatar", area: SecuritySettings.ExtensionName))
                .AddMenu("log", it => it.Texted("活动日志").Page("/Admin/User/Log", area: SecuritySettings.ExtensionName))
            );
        }
    }
}