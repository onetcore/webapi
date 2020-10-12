using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Gentings.Extensions.Emails;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages
{
    /// <summary>
    /// 忘记密码页面。
    /// </summary>
    public class ForgotPasswordModel : ModelBase
    {
        /// <summary>
        /// 忘记密码模型。
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }
        /// <summary>
        /// 输入模型。
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "电子邮件不能为空！")]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            /// 验证码。
            /// </summary>
            public string Code { get; set; }
        }

        private readonly IUserManager _userManager;
        private readonly IEmailManager _emailSender;
        /// <summary>
        /// 初始化类<see cref="ForgotPasswordModel"/>。
        /// </summary>
        /// <param name="userManager">用户管理实例。</param>
        /// <param name="emailSender">电子邮件发送接口。</param>
        public ForgotPasswordModel(IUserManager userManager, IEmailManager emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        /// <summary>
        /// 发送电子邮件验证连接。
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Settings.ValidCode && !IsCodeValid("forgot", Input.Code))
                {
                    ModelState.AddModelError("Input.Code", "验证码不正确！");
                    return Page();
                }

                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page("/ResetPassword",
                    pageHandler: null,
                    values: new { code, area = SecuritySettings.ExtensionName },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    user.Id,
                    Input.Email,
                    "重置密码",
                    $"重置密码链接： <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>点击这里</a>...");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
