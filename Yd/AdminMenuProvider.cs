using Gentings.AspNetCore.AdminMenus;

namespace Yd
{
    public class AdminMenuProvider : MenuProvider
    {
        /// <summary>
        /// 初始化菜单实例。
        /// </summary>
        /// <param name="root">根目录菜单。</param>
        public override void Init(MenuItem root)
        {
            root.AddMenu("dashboard", item => item.Texted("控制面板", "home").Page("/Admin/Index"));
            root.AddMenu("sys", item => item.AddMenu("settings", it => it.Texted("系统配置").Page("/Admin/Settings")));
        }
    }
}