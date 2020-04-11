using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Admin.User
{
    /// <summary>
    /// 更新头像。
    /// </summary>
    public class AvatarModel : ModelBase
    {
        private readonly IUserManager _userManager;

        public AvatarModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public void OnGet()
        {
            Input = new InputModel { UserId = UserId };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Input.AvatarFile == null)
                return RedirectToErrorPage("请选择文件后再上传文件！");
            var result = await _userManager.UploadAvatarAsync(Input.UserId, Input.AvatarFile);
            if (result != null)
                return RedirectToSuccessPage("你已经成功更新了头像！");
            return ErrorPage("更新头像失败！");
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public IFormFile AvatarFile { get; set; }

            public int UserId { get; set; }
        }
    }
}