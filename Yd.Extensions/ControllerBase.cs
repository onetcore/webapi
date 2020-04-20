using Gentings.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Roles;

namespace Yd.Extensions
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ControllerBase : Gentings.Extensions.AspNetCore.ControllerBase
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

    /// <summary>
    /// 控制器基类。
    /// </summary>
    [Authorize]
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    public abstract class AdminControllerBase : ControllerBase
    {

    }

    /// <summary>
    /// 控制器基类。
    /// </summary>
    [Authorize]
    [Area("account")]
    [Route("api/[area]/[controller]")]
    public abstract class AccountControllerBase : ControllerBase
    {

    }

}