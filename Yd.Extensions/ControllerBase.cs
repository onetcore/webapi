using Gentings.Identity;
using Yd.Extensions.Security;

namespace Yd.Extensions
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    public abstract class ControllerBase : Gentings.Identity.ControllerBase
    {
        private User _user;
        /// <summary>
        /// 当前登录用户Id。
        /// </summary>
        protected new User User => _user ??= HttpContext.GetUser<User>();

        private Role _role;
        /// <summary>
        /// 当前用户的最大角色实例。
        /// </summary>
        protected Role Role => _role ??= GetRequiredService<IRoleManager>().FindById(User.RoleId);
    }
}