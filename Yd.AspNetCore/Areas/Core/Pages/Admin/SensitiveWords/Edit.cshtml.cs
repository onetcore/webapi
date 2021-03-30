using System.Threading.Tasks;
using Gentings.Extensions.SensitiveWords;
using Gentings.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using Yd.AspNetCore.Core;

namespace Yd.AspNetCore.Areas.Core.Pages.Admin.SensitiveWords
{
    /// <summary>
    /// 编辑敏感词汇。
    /// </summary>
    [PermissionAuthorize(CorePermissions.EditSensitive)]
    public class EditModel : AdminModelBase
    {
        private readonly ISensitiveWordManager _sensitiveWordManager;
        public EditModel(ISensitiveWordManager sensitiveWordManager)
        {
            _sensitiveWordManager = sensitiveWordManager;
        }
        [BindProperty]
        public SensitiveWord Input { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id > 0)
            {
                Input = _sensitiveWordManager.Find(id);
                if (Input == null)
                    return NotFound();
            }
            else
                Input = new SensitiveWord();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Word))
            {
                ModelState.AddModelError("Input.Word", "敏感词不能为空！");
                return Error();
            }

            var result = await _sensitiveWordManager.SaveAsync(Input);
            return Json(result, Input.Word);
        }
    }
}