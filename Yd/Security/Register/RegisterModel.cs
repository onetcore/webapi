using System;

namespace Yd.Security.Register
{
    /// <summary>
    /// 注册模型。
    /// </summary>
    public class RegisterModel
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
        /// 电子邮件。
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// 电话号码。
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 短信验证码。
        /// </summary>
        public string Captcha { get; set; }

        /// <summary>
        /// 区码如：+86。
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 确认密码。
        /// </summary>
        public string Confirm { get; set; }

        /// <summary>
        /// 邀请码。
        /// </summary>
        public string InviteKey { get; set; }
    }
}