using System.Threading.Tasks;
using Gentings.Extensions.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.RazorPages.Areas.Core.Pages.Admin.DictionarySettings
{
    public class EditModel : ModelBase
    {
        private readonly ISettingDictionaryManager _settingManager;
        public EditModel(ISettingDictionaryManager settingManager)
        {
            _settingManager = settingManager;
        }

        [BindProperty]
        public SettingDictionary Input { get; set; }

        public IActionResult OnGet(int id, int pid)
        {
            if (id > 0)
            {
                Input = _settingManager.Find(id);
                if (Input == null)
                    return NotFound();
            }
            else
                Input = new SettingDictionary { ParentId = pid };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Name))
            {
                ModelState.AddModelError("Input.Name", "名称不能为空！");
                return Error();
            }

            var result = await _settingManager.SaveAsync(Input);
            if (result)
            {
                await LogResultAsync(result, "字典实例，{0}：{1}。", Input.Name, Input.Value);
            }

            return Json(result, Input.Value);
        }
    }
}