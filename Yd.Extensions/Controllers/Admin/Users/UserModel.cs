using System;

namespace Yd.Extensions.Controllers.Admin.Users
{
    /// <summary>
    /// 用户模型。
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 用户Id。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名称。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称。
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 电话号码。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 电子邮件。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 注册IP。
        /// </summary>
        public string CreatedIP { get; set; }

        /// <summary>
        /// 登录IP。
        /// </summary>
        public string LoginIP { get; set; }

        /// <summary>
        /// 是否激活锁定功能。
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 锁定时间。
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// 最后登录时间。
        /// </summary>
        public DateTimeOffset? LastLoginDate { get; set; }
    }
}