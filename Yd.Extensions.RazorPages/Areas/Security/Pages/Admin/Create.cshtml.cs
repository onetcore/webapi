using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Gentings.Identity;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Admin
{
    /// <summary>
    /// 添加用户。
    /// </summary>
    [PermissionAuthorize(Security.Permissions.Users)]
    public class CreateModel : ModelBase
    {
        private readonly IUserManager _userManager;

        public CreateModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public class InputModel
        {
            /// <summary>
            /// 登录名称。
            /// </summary>
            [Display(Name = "登录名称")]
            [Required(ErrorMessage = "{0}不能为空！")]
            [RegularExpression("[@a-zA-Z][a-zA-Z_0-9]{4,15}", ErrorMessage = "{0}必须以字母或者@开头，由字母和数字以及下划线组合5到16个字符组成！")]
            public string UserName { get; set; }

            public string NickName { get; set; }

            [Display(Name = "电子邮件")]
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

            public string PhoneNumber { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.UserName,
                    NormalizedUserName = _userManager.NormalizeName(Input.UserName),
                    NickName = Input.NickName ?? Input.UserName,
                    PasswordHash = Input.Password,
                    Email = Input.Email,
                    NormalizedEmail = _userManager.NormalizeEmail(Input.Email),
                    PhoneNumber = Input.PhoneNumber,
                    EmailConfirmed = !string.IsNullOrEmpty(Input.Email) && !Settings.RequireConfirmedEmail,
                    PhoneNumberConfirmed = !string.IsNullOrEmpty(Input.PhoneNumber) && !Settings.RequireConfirmedPhoneNumber,
                    LockoutEnabled = true
                };
                user.TwoFactorEnabled = (!string.IsNullOrEmpty(Input.Email) || !string.IsNullOrEmpty(Input.PhoneNumber)) && Settings.RequiredTwoFactorEnabled;
                var result = await _userManager.IsDuplicatedAsync(user);
                if (!result.Succeeded)
                    return Error(result.ToErrorString());
                result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    Log($"添加了账户“{Input.UserName}”的信息！");
                    return Success("你已经成功添加了账户！");
                }
                return Error(result.ToErrorString());
            }
            return Error();
        }
    }
}