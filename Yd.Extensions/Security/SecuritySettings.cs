﻿using System.ComponentModel;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 安全配置。
    /// </summary>
    public class SecuritySettings
    {
        /// <summary>
        /// 推广链接个数。
        /// </summary>
        public const int AliasCount = 5;

        /// <summary>
        /// 扩展名称（区域名称）。
        /// </summary>
        public const string ExtensionName = "security";
        
        /// <summary>
        /// 是否需要确认电子邮件。
        /// </summary>
        [DisplayName("确认电子邮件")]
        public bool RequiredEmailConfirmed { get; set; }

        /// <summary>
        /// 是否需要确认电话号码。
        /// </summary>
        [DisplayName("确认电话号码")]
        public bool RequiredPhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 是否需要二次验证。
        /// </summary>
        [DisplayName("二次登录验证")]
        public bool RequiredTwoFactorEnabled { get; set; }

        /// <summary>
        /// 开放注册。
        /// </summary>
        [DisplayName("开放注册")]
        public bool Registrable { get; set; }

        /// <summary>
        /// 登录后的默认转向。
        /// </summary>
        [DisplayName("登录后转向")]
        public LoginDirection LoginDirection { get; set; }

        /// <summary>
        /// 是否开启验证码。
        /// </summary>
        [DisplayName("开启验证码")]
        public bool ValidCode { get; set; }

        /// <summary>
        /// 登录页面背景图片。
        /// </summary>
        [DisplayName("登录页面背景图片")]
        public string LoginBg { get; set; } = "/security/images/login.jpg";
    }
}