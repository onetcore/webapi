using System.Threading.Tasks;
using Gentings.Extensions.Settings;
using Gentings.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using Yd.AspNetCore.Core;

namespace Yd.AspNetCore.Areas.Core.Pages.Admin.NamedStrings
{
    /// <summary>
    /// 编辑字典。
    /// </summary>
    [PermissionAuthorize(CorePermissions.EditNamedStrings)]
    public class EditModel : AdminModelBase
    {
        private readonly INamedStringManager _stringManager;
        public EditModel(INamedStringManager stringManager)
        {
            _stringManager = stringManager;
        }

        [BindProperty]
        public NamedString Input { get; set; }

        public IActionResult OnGet(int id, int pid)
        {
            if (id > 0)
            {
                Input = _stringManager.Find(id);
                if (Input == null)
                    return NotFound();
            }
            else
                Input = new NamedString { ParentId = pid };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Name))
            {
                ModelState.AddModelError("Input.Name", "名称不能为空！");
                return Error();
            }

            var result = await _stringManager.SaveAsync(Input);
            return Json(result, Input.Value);
        }
    }
}