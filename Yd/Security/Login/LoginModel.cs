namespace Yd.Security.Login
{
    /// <summary>
    /// 登录模型。
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 用户名。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 记住登录状态。
        /// </summary>
        public bool AutoLogin { get; set; }

        /// <summary>
        /// 电话号码。
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 短信验证码。
        /// </summary>
        public string Captcha { get; set; }

        /// <summary>
        /// 登录类型。
        /// </summary>
        public string Type { get; set; }
    }
}