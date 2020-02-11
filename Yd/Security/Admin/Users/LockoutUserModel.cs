using System;

namespace Yd.Security.Admin.Users
{
    /// <summary>
    /// 锁定用户模型。
    /// </summary>
    public class LockoutUserModel
    {
        /// <summary>
        /// 用户Id。
        /// </summary>
        public int[] Ids { get; set; }

        /// <summary>
        /// 锁定时间。
        /// </summary>
        public DateTimeOffset LockoutEnd { get; set; }
    }
}