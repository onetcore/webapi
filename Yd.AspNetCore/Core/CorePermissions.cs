﻿using Gentings.Security.Permissions;

namespace Yd.AspNetCore.Core
{
    /// <summary>
    /// 核心权限列表。
    /// </summary>
    public class CorePermissions : PermissionProvider
    {
        /// <summary>
        /// 初始化权限实例。
        /// </summary>
        protected override void Init()
        {
            Add("settings", "网站配置", "允许用户管理网站配置信息！");
            Add("task", "后台服务", "允许管理后台服务相关操作!");
            Add("taskinterval", "配置定时模式", "允许配置后台服务定时模式相关操作!");
            Add("storages", "文件管理", "允许管理文件存储相关操作!");
            Add("sensitive", "敏感词汇管理", "允许访问敏感词汇相关操作!");
            Add("namedstrings", "字典管理", "允许访问字典相关操作!");
            Add("editsensitive", "编辑敏感词汇", "允许编辑敏感词汇相关操作!");
            Add("editnamedstrings", "编辑字典", "允许编辑字典相关操作!");
        }

        /// <summary>
        /// 网站配置。
        /// </summary>
        public const string SiteSettings = "core.settings";

        /// <summary>
        /// 后台服务。
        /// </summary>
        public const string Task = "core.task";

        /// <summary>
        /// 配置后台服务定时模式。
        /// </summary>
        public const string TaskInterval = "core.taskinterval";

        /// <summary>
        /// 敏感词汇。
        /// </summary>
        public const string Sensitive = "core.sensitive";

        /// <summary>
        /// 编辑敏感词汇。
        /// </summary>
        public const string EditSensitive = "core.editsensitive";

        /// <summary>
        /// 字典。
        /// </summary>
        public const string NamedStrings = "core.namedstrings";

        /// <summary>
        /// 编辑字典。
        /// </summary>
        public const string EditNamedStrings = "core.editnamedstrings";
    }
}