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
        /// 邀请码。
        /// </summary>
        public Guid? InviteKey { get; set; }

        /// <summary>
        /// 推广Id。
        /// </summary>
        public int Intro { get; set; }
    }
}