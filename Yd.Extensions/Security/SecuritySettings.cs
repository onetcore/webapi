using Gentings.Identity;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 安全配置。
    /// </summary>
    public class SecuritySettings : IdentitySettings
    {
        /// <summary>
        /// 扩展名称（区域名称）。
        /// </summary>
        public const string ExtensionName = "security";

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