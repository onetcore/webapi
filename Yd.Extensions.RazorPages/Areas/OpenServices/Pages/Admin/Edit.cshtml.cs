using System;
using System.Threading.Tasks;
using Gentings;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Admin
{
    [PermissionAuthorize(Permissions.Settings)]
    public class EditModel : ModelBase
    {
        private readonly IApplicationManager _applicationManager;

        public EditModel(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        [BindProperty]
        public Application Input { get; set; }

        public void OnGet(Guid id)
        {
            Input = _applicationManager.Find(id) ?? new Application();
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(Input.Name))
            {
                ModelState.AddModelError("Input.Name", "名称不能为空！");
                return Error();
            }

            var application = await _applicationManager.FindAsync(Input.Id);
            if (application != null)
            {
                application.AppSecret = Input.AppSecret;
                application.UserId = Input.UserId;
                application.Summary = Input.Summary;
                application.Name = Input.Name;
            }
            else
            {
                application = Input;
            }
            if (Input.UserId == 0)
            {
                ModelState.AddModelError("Input.UserId", "请选择用户后再进行操作！");
                return Error();
            }

            var result = await _applicationManager.SaveAsync(Input);
            LogResult(result, Input.Name);
            return Json(result, Input.Name);
        }

        public IActionResult OnPostGeneral()
        {
            return Success(new { AppSecret = Cores.GeneralKey(128) });
        }
    }
}