using Gentings.AspNetCore.AdminMenus;

namespace Yd.Pages.Admin
{
    public class AdminMenu : MenuProvider
    {
        /// <summary>
        /// 初始化菜单实例。
        /// </summary>
        /// <param name="root">根目录菜单。</param>
        public override void Init(MenuItem root)
        {
            root.AddMenu("dashboard", item => item.Texted("控制面板", "home").Page("/Admin/Index"));
        }
    }
}