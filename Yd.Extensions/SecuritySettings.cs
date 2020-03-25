namespace Yd.Extensions
{
    /// <summary>
    /// 安全配置。
    /// </summary>
    public class SecuritySettings
    {
        /// <summary>
        /// 扩展名称（区域名称）。
        /// </summary>
        public const string ExtensionName = "security";
        
        /// <summary>
        /// 是否需要确认电子邮件。
        /// </summary>
        public bool RequiredEmailConfirmed { get; set; }

        /// <summary>
        /// 是否需要确认电话号码。
        /// </summary>
        public bool RequiredPhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 是否需要二次验证。
        /// </summary>
        public bool RequiredTwoFactorEnabled { get; set; }

        /// <summary>
        /// 开放注册。
        /// </summary>
        public bool Registrable { get; set; }

        /// <summary>
        /// 登录后的默认转向。
        /// </summary>
        public LoginDirection LoginDirection { get; set; }

        /// <summary>
        /// 是否开启验证码。
        /// </summary>
        public bool ValidCode { get; set; }

        /// <summary>
        /// 登录页面背景图片。
        /// </summary>
        public string LoginBg { get; set; } = "/security/images/login.jpg";
    }
}