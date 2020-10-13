using System.Threading.Tasks;
using Gentings.Extensions.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.RazorPages.Areas.Core.Controllers
{
    /// <summary>
    /// 通知控制器。
    /// </summary>
    [Authorize]
    public class NotifierController : ControllerBase
    {
        private readonly INotificationManager _notificationManager;

        public NotifierController(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        /// <summary>
        /// 获取通知。
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var notifications = await _notificationManager.LoadAsync();
            return OkResult(notifications);
        }

        /// <summary>
        /// 确认。
        /// </summary>
        /// <param name="id">通知Id。</param>
        [HttpPost]
        public async Task<IActionResult> Confirmed(int id)
        {
            await _notificationManager.SetStatusAsync(id, NotificationStatus.Confirmed);
            return OkResult();
        }

        /// <summary>
        /// 清空当前用户的通知。
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            await _notificationManager.ClearAsync();
            return OkResult();
        }
    }
}