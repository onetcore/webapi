using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yd.Extensions.Security;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages
{
    /// <summary>
    /// 登录模型。
    /// </summary>
    public class LoginModel : ModelBase
    {
        private readonly IUserManager _userManager;
        /// <summary>
        /// 用户登录输入模型。
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// 登录用户模型。
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// 用户名。
            /// </summary>
            [Required(ErrorMessage = "用户名不能为空!")]
            public string UserName { get; set; }

            /// <summary>
            /// 密码。
            /// </summary>
            [Required(ErrorMessage = "密码不能为空!")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            /// 验证码。
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// 登录状态。
            /// </summary>
            public bool RememberMe { get; set; }
        }

        /// <summary>
        /// 外部登录列表。
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// 返回地址。
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 错误消息。
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        public LoginModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.GetDirection(Settings.LoginDirection);

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _userManager.SignInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (Settings.ValidCode && !IsCodeValid("login", Input.Code))
                {
                    ModelState.AddModelError("Input.Code", "验证码不正确！");
                    return Page();
                }

                returnUrl ??= Url.GetDirection(Settings.LoginDirection);
                Input.UserName = Input.UserName.Trim();
                Input.Password = Input.Password.Trim();

                var user = await _userManager.FindByNameAsync(Input.UserName);
                if (user == null)
                    return ErrorPage("用户或者密码错误！");
                var result = await _userManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe);
                if (result.Succeeded)
                {
                    Log(user.Id, "通过账号登录了系统");
                    Response.Cookies.Delete("login");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    Logger.LogWarning($"账号：{Input.UserName}被锁定。");
                    return RedirectToPage("./Lockout");
                }

                ModelState.AddModelError(string.Empty, "用户名或密码错误。");
                return Page();
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
