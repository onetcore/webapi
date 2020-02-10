using Gentings.Identity;
using Yd.Extensions.Security;

namespace Yd.Extensions
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    public abstract class ControllerBase : Gentings.AspNetCore.ControllerBase
    {
        private int? _userId;
        /// <summary>
        /// 当前登录用户Id。
        /// </summary>
        protected int UserId => _userId ?? (_userId = HttpContext.User.GetUserId()).Value;

        private string _userName;
        /// <summary>
        /// 当前登录用户Id。
        /// </summary>
        protected string UserName => _userName ??= HttpContext.User.GetUserName();

        private User _user;
        /// <summary>
        /// 当前登录用户Id。
        /// </summary>
        protected new User User => _user ??= HttpContext.GetUser<User>();
    }
}