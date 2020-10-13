using System;
using System.Threading.Tasks;
using Gentings;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.ApiOpenServices.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Account
{
    [PermissionAuthorize(OpenServicePermissions.Create)]
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
                application.UserId = UserId;
                application.Summary = Input.Summary;
                application.Name = Input.Name;
            }
            else
            {
                application = Input;
            }

            var result = await _applicationManager.SaveAsync(application);
            LogResult(result, Input.Name);
            return Json(result, Input.Name);
        }

        public IActionResult OnPostGeneral()
        {
            return Success(new { AppSecret = Cores.GeneralKey(128) });
        }
    }
}