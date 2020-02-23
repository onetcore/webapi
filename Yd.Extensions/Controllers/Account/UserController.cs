using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.Controllers.Account
{
    /// <summary>
    /// 用户控制器。
    /// </summary>
    public class UserController : ApiAccountControllerBase
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
            var user = await _userManager.GetCachedUserAsync(UserId);
            if (user == null)
                return BadRequest();
            return Ok(user);
        }

        /// <summary>
        /// 退出登录。
        /// </summary>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userManager.SignOutAsync();
            Log("退出了登录。");
            return OkResult();
        }
    }
}