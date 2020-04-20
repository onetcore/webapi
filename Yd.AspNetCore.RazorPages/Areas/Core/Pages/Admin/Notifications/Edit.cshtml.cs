using System.Threading.Tasks;
using Gentings.Extensions.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.RazorPages.Areas.Core.Pages.Admin.Notifications
{
    public class EditModel : ModelBase
    {
        private readonly INotificationTypeManager _typeManager;

        public EditModel(INotificationTypeManager typeManager)
        {
            _typeManager = typeManager;
        }

        [BindProperty]
        public NotificationType Input { get; set; }

        public void OnGet(int id)
        {
            Input = _typeManager.Find(id) ?? new NotificationType();
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(Input.Name))
            {
                ModelState.AddModelError("Input.Name", "名称不能为空！");
                return Error();
            }

            var result = await _typeManager.SaveAsync(Input);
            if (result)
            {
                await LogResultAsync(result, "通知类型：{0}。", Input.Name);
            }

            return Json(result, Input.Name);
        }
    }
}