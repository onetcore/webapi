using System.Threading.Tasks;
using Gentings.Identity;
using Gentings.Identity.Events;
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
        private readonly IEventLogger _logger;

        /// <summary>
        /// 初始化类<see cref="UserController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="logger">用户事件日志。</param>
        public UserController(IUserManager userManager, IEventLogger logger)
        {
            _userManager = userManager;
            _logger = logger;
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

        /// <summary>
        /// 退出登录。
        /// </summary>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _userManager.SignOutAsync();
            _logger.LogUser("退出了登录。");
            return OkResult();
        }
    }
}