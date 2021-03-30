using System.Collections.Generic;
using System.Threading.Tasks;
using Gentings.Security.Notifications;
using Gentings.Security.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.Notifications
{
    /// <summary>
    /// 通知管理。
    /// </summary>
    [PermissionAuthorize(SecurityPermissions.Notifier)]
    public class IndexModel : ModelBase
    {
        private readonly INotificationManager _notificationManager;
        public IndexModel(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        /// <summary>
        /// 通知类型列表。
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public NotificationQuery Query { get; set; }

        /// <summary>
        /// 通知列表。
        /// </summary>
        public IEnumerable<Notification> Items { get; private set; }

        public async Task OnGet()
        {
            Items = await _notificationManager.LoadAsync(Query);
        }

        public IActionResult OnPostDelete(int[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return Error("请先选择通知类型后在进行删除操作！");
            }

            var result = _notificationManager.Delete(ids);
            return Json(result, "通知");
        }
    }
}