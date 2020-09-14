using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.Extensions.RazorPages.Areas.Security.Controllers
{
    /// <summary>
    /// 用户控制器。
    /// </summary>
    [Authorize]
    public class AccountController : Gentings.Extensions.AspNetCore.ControllerBase
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// 初始化类<see cref="AccountController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 退出登录。
        /// </summary>
        [Route("logout")]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _userManager.SignOutAsync();
            await LogAsync("退出了登录。");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            return LocalRedirect("/");
        }
    }
}