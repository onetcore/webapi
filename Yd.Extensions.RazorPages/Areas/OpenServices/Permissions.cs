using Gentings.Identity.Permissions;
using Yd.Extensions.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices
{
    /// <summary>
    /// 权限。
    /// </summary>
    public class Permissions : PermissionProvider
    {
        /// <summary>
        /// 分类。
        /// </summary>
        public override string Category { get; } = OpenServiceSettings.ExtensionName;

        /// <summary>
        /// 应用管理。
        /// </summary>
        public const string OpenServices = "openservices.index";

        /// <summary>
        /// 应用配置管理。
        /// </summary>
        public const string Settings = "openservices.settings";

        /// <summary>
        /// 初始化权限实例。
        /// </summary>
        protected override void Init()
        {
            Add("index", "应用管理", "允许管理应用相关操作!");
            Add("settings", "应用配置", "允许管理应用配置相关操作!");
        }
    }
}