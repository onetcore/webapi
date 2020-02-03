using System.Threading.Tasks;
using Gentings.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
using ControllerBase = Gentings.AspNetCore.ControllerBase;

namespace Yd.Security.Account
{
    /// <summary>
    /// 用户控制器。
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        /// <summary>
        /// 初始化类<see cref="UserController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 获取当前登录用户。
        /// </summary>
        /// <returns>返回当前登录用户实例。</returns>
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userid = HttpContext.User.GetUserId();
            if (userid == 0)
                return BadRequest();
            var user = await _userManager.GetUserAsync(userid);
            if (user == null)
                return BadRequest();
            return Ok(user);
        }
    }
}