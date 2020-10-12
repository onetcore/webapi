﻿using Gentings.Identity.Permissions;
using Yd.Extensions.Security;

namespace Yd.Extensions.RazorPages.Areas.Security
{
    /// <summary>
    /// 权限。
    /// </summary>
    public class SecurityPermissions : PermissionProvider
    {
        /// <summary>
        /// 分类。
        /// </summary>
        public override string Category => SecuritySettings.ExtensionName;

        /// <summary>
        /// 初始化权限实例。
        /// </summary>
        protected override void Init()
        {
            Add("manager", "管理用户", "对用户账户进行管理操作！");
            Add("setrole", "设置用户角色", "对用户账户进行角色管理操作！");
            Add("roles", "管理角色", "对角色进行管理操作！");
            Add("permissions", "权限管理", "对所有权限进行管理操作！");
            Add("logs", "日志管理", "管理用户操作日志！");
            Add("settings", "用户配置", "管理用户配置权限！");
        }

        /// <summary>
        /// 管理用户权限。
        /// </summary>
        public const string Users = "security.manager";

        /// <summary>
        /// 管理用户组权限。
        /// </summary>
        public const string Roles = "security.roles";

        /// <summary>
        /// 设置用户组。
        /// </summary>
        public const string SetRoles = "security.setrole";

        /// <summary>
        /// 权限管理权限。
        /// </summary>
        public const string PermissionManager = "security.permissions";

        /// <summary>
        /// 管理日志权限。
        /// </summary>
        public const string Logs = "security.logs";

        /// <summary>
        /// 用户配置权限。
        /// </summary>
        public const string Settings = "security.settings";
    }
}