﻿using Gentings.Extensions.Settings;
using Gentings.Identity;
using Yd.Extensions.Roles;
using Yd.Extensions.Security.Properties;

namespace Yd.Extensions.Security.Areas.Security
{
    /// <summary>
    /// 页面模型基类。
    /// </summary>
    public abstract class ModelBase : Gentings.AspNetCore.RazorPages.ModelBase
    {
        private SecuritySettings _settings;
        /// <summary>
        /// 安全配置。
        /// </summary>
        public SecuritySettings Settings => _settings ??= GetRequiredService<ISettingsManager>()
            .GetSettings<SecuritySettings>();

        private User _user;
        /// <summary>
        /// 当前登录用户Id。
        /// </summary>
        public new User User => _user ??= HttpContext.GetUser<User>();

        private Role _role;
        /// <summary>
        /// 当前用户的最大角色实例。
        /// </summary>
        public Role Role => _role ??= GetRequiredService<IRoleManager>().GetCacheRole(User.RoleId);

        /// <summary>
        /// 事件类型。
        /// </summary>
        protected override string EventType => Resources.EventType;
    }
}