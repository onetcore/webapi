using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Gentings.Extensions.Emails;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.AspNetCore.Security.Areas.Security.Pages
{
    /// <summary>
    /// 注册。
    /// </summary>
    public class RegisterModel : ModelBase
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            /// <summary>
            /// 登录名称。
            /// </summary>
            [Display(Name = "登录名称")]
            [Required(ErrorMessage = "{0}不能为空！")]
            [RegularExpression("[@a-zA-Z][a-zA-Z_0-9]{4,15}", ErrorMessage = "{0}必须以字母或者@开头，由字母和数字以及下划线组合5到16个字符组成！")]
            public string UserName { get; set; }

            [Display(Name = "电子邮件")]
            [Required(ErrorMessage = "{0}不能为空！")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "密码")]
            [Required(ErrorMessage = "{0}不能为空！")]
            [StringLength(16, ErrorMessage = "{0}的长度必须大于{2}， 小于{1}个字符！", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "确认密码")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "{0}和{1}不匹配！")]
            public string ConfirmPassword { get; set; }

            /// <summary>
            /// 验证码。
            /// </summary>
            public string Code { get; set; }
        }

        private readonly IUserManager _userManager;
        private readonly IEmailManager _emailSender;

        public RegisterModel(
            IUserManager userManager,
            IEmailManager emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (!Settings.Registrable)
                return NotFound();
            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!Settings.Registrable)
                return NotFound();
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                if (Settings.ValidCode && !IsCodeValid("register", Input.Code))
                {
                    ModelState.AddModelError("Input.Code", "验证码不正确！");
                    return Page();
                }

                var user = new User();
                user.UserName = Input.UserName;
                user.NormalizedUserName = _userManager.NormalizeName(Input.UserName);
                user.Email = Input.Email;
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await Events.LogAsync(@event =>
                    {
                        @event.UserId = user.Id;
                        @event.Message = "注册了新用户。";
                    }, EventType);

                    if (Settings.RequireConfirmedEmail)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page("/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(user.Id, Input.Email, "激活账号",
                            $"激活账号：<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>点击这里</a>...");
                    }
                    else
                        await _userManager.SignInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
