using Gentings.AspNetCore.AdminMenus;
using Yd.Extensions.Controllers.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Admin
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
            root.AddMenu("open", item => item.Texted("开放平台", "layers").Page("/Admin/Index", area: OpenServiceSettings.ExtensionName)
                .AddMenu("apps", it=>it.Texted("应用程序列表").Page("/Admin/Index", area: OpenServiceSettings.ExtensionName).Allow(Permissions.OpenServices))
                .AddMenu("services", it => it.Texted("开放服务列表").Page("/Admin/Services/Index", area: OpenServiceSettings.ExtensionName).Allow(Permissions.OpenServices))
            );
        }
    }
}