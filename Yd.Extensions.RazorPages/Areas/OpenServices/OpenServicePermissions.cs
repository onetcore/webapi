using Gentings.Identity.Permissions;
using Yd.Extensions.Controllers.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices
{
    /// <summary>
    /// 权限列表。
    /// </summary>
    public class OpenServicePermissions : PermissionProvider
    {
        /// <summary>
        /// 分类。
        /// </summary>
        public override string Category => OpenServiceSettings.ExtensionName;

        /// <summary>
        /// 初始化权限实例。
        /// </summary>
        protected override void Init()
        {
            Add("view", "访问开放平台", "允许用户访问开放平台相关功能！");
            Add("create", "添加应用程序", "允许用户添加应用程序相关功能！");
            Add("update", "编辑应用程序", "允许用户编辑应用程序相关功能！");
            Add("delete", "删除应用程序", "允许用户删除应用程序相关功能！");
            Add("setting", "配置API服务", "允许用户配置API服务相关功能！");
        }

        /// <summary>
        /// 访问开放平台。
        /// </summary>
        public const string View = OpenServiceSettings.ExtensionName + ".view";

        /// <summary>
        /// 添加应用程序。
        /// </summary>
        public const string Create = OpenServiceSettings.ExtensionName + ".create";

        /// <summary>
        /// 编辑应用程序。
        /// </summary>
        public const string Update = OpenServiceSettings.ExtensionName + ".update";

        /// <summary>
        /// 删除应用程序。
        /// </summary>
        public const string Delete = OpenServiceSettings.ExtensionName + ".delete";

        /// <summary>
        /// 配置API服务。
        /// </summary>
        public const string Setting = OpenServiceSettings.ExtensionName + ".setting";
    }
}