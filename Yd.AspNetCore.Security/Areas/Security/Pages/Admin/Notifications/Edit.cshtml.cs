using System.Threading.Tasks;
using Gentings.Security.Notifications;
using Gentings.Security.Permissions;
using Gentings.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.Notifications
{
    /// <summary>
    /// 编辑通知。
    /// </summary>
    [PermissionAuthorize(SecurityPermissions.EditNotifier)]
    public class EditModel : ModelBase
    {
        private readonly INotificationManager _notificationManager;
        private readonly IMediaDirectory _mediaDirectory;

        public EditModel(INotificationManager notificationManager, IMediaDirectory mediaDirectory)
        {
            _notificationManager = notificationManager;
            _mediaDirectory = mediaDirectory;
        }

        [BindProperty]
        public Notification Input { get; set; }

        public void OnGet(int id)
        {
            Input = _notificationManager.Find(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Title))
            {
                ModelState.AddModelError("Input.Title", "标题不能为空！");
                return Error();
            }

            Input.SendId = UserId;
            var result = await _notificationManager.SaveAsync(Input);
            return Json(result, Input.Title);
        }

        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            var result = await _mediaDirectory.UploadAsync(file, nameof(Notification));
            return Json(result);
        }
    }
}