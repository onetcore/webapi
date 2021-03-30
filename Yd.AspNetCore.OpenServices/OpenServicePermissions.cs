using Gentings.Extensions.OpenServices;
using Gentings.Security.Permissions;

namespace Yd.AspNetCore.OpenServices
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
            Add(Names.View, "访问开放平台", "允许用户访问开放平台相关功能！");
            Add(Names.Create, "添加应用程序", "允许用户添加应用程序相关功能！");
            Add(Names.Update, "编辑应用程序", "允许用户编辑应用程序相关功能！");
            Add(Names.Delete, "删除应用程序", "允许用户删除应用程序相关功能！");
            Add(Names.Setting, "配置API服务", "允许用户配置API服务相关功能！");
        }

        /// <summary>
        /// 访问开放平台。
        /// </summary>
        public const string View = OpenServiceSettings.ExtensionName + DotNames.View;

        /// <summary>
        /// 添加应用程序。
        /// </summary>
        public const string Create = OpenServiceSettings.ExtensionName + DotNames.Create;

        /// <summary>
        /// 编辑应用程序。
        /// </summary>
        public const string Update = OpenServiceSettings.ExtensionName + DotNames.Update;

        /// <summary>
        /// 删除应用程序。
        /// </summary>
        public const string Delete = OpenServiceSettings.ExtensionName + DotNames.Delete;

        /// <summary>
        /// 配置API服务。
        /// </summary>
        public const string Setting = OpenServiceSettings.ExtensionName + DotNames.Setting;
    }
}