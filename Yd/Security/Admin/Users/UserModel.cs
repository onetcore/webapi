﻿using System;

namespace Yd.Security.Admin.Users
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
        /// 真实姓名。
        /// </summary>
        public string RealName { get; set; }

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
        /// 最后登录时间。
        /// </summary>
        public DateTimeOffset? LastLoginDate { get; set; }
    }
}