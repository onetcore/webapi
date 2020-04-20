using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Gentings.Identity;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Admin
{
    [PermissionAuthorize(Security.Permissions.Users)]
    public class EditModel : ModelBase
    {
        private readonly IUserManager _userManager;

        public EditModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public class InputModel
        {
            public InputModel() { }

            public InputModel(Extensions.User currentUser)
            {
                UserId = currentUser.Id;
                NickName = currentUser.NickName;
                PhoneNumber = currentUser.PhoneNumber;
                Email = currentUser.Email;
            }

            /// <summary>
            /// 用户Id。
            /// </summary>
            public int UserId { get; set; }

            [Required(ErrorMessage = "昵称不能为空！")]
            public string NickName { get; set; }

            public string PhoneNumber { get; set; }

            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task OnGetAsync(int id)
        {
            Input = new InputModel(await _userManager.FindByIdAsync(id));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(Input.UserId);
                if (user == null)
                    return Error("未找到任何关联用户！");
                var differ = GetObjectDiffer(user);
                if (user.NickName != Input.NickName)
                {
                    user.NickName = Input.NickName;
                    var duplicated = await _userManager.IsDuplicatedAsync(user);
                    if (!duplicated.Succeeded)
                        return Error(duplicated.ToErrorString());
                }

                if (user.Email != Input.Email)
                {
                    user.Email = Input.Email;
                    user.NormalizedEmail = _userManager.NormalizeEmail(Input.Email);
                    user.EmailConfirmed = !string.IsNullOrEmpty(Input.Email) && !Settings.RequireConfirmedEmail;
                }

                if (user.PhoneNumber != Input.PhoneNumber)
                {
                    user.PhoneNumber = Input.PhoneNumber;
                    user.PhoneNumberConfirmed = !string.IsNullOrEmpty(Input.PhoneNumber) && !Settings.RequireConfirmedPhoneNumber;
                }

                if (differ.IsDifference(user))
                {
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        Log($"更新了用户“{user.UserName}”的信息：{differ}");
                        return Success("你已经成功更新了用户信息！");
                    }
                    return Error(result.ToErrorString());
                }
                return Success("你已经成功更新了用户信息！");
            }
            return Error();
        }
    }
}