﻿using Gentings.Extensions.Emails;
using Gentings.Security.Permissions;

namespace Yd.AspNetCore.Emails
{
    /// <summary>
    /// 权限。
    /// </summary>
    public class EmailPermissions : PermissionProvider
    {
        /// <summary>
        /// 分类。
        /// </summary>
        public override string Category { get; } = EmailSettings.ExtensionName;

        /// <summary>
        /// 邮件管理。
        /// </summary>
        public const string Email = "emails.index";

        /// <summary>
        /// 邮件配置管理。
        /// </summary>
        public const string Settings = "emails.settings";

        /// <summary>
        /// 发送邮件。
        /// </summary>
        public const string Send = "emails.send";

        /// <summary>
        /// 初始化权限实例。
        /// </summary>
        protected override void Init()
        {
            Add("index", "邮件管理", "允许管理邮件相关操作!");
            Add("send", "发送邮件", "允许发送邮件相关操作!");
            Add("settings", "邮件配置", "允许管理邮件配置相关操作!");
        }
    }
}