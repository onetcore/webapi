using System.Collections.Generic;
using System.Linq;
using Gentings.Extensions.Notifications;
using Gentings.Extensions.Settings;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.RazorPages.Areas.Core.Pages.Admin.Notifications
{
    /// <summary>
    /// 通知管理。
    /// </summary>
    [PermissionAuthorize(CorePermissions.Notifier)]
    public class IndexModel : ModelBase
    {
        private readonly INotificationTypeManager _typeManager;
        private readonly ISettingsManager _settingsManager;

        public IndexModel(INotificationTypeManager typeManager, ISettingsManager settingsManager)
        {
            _typeManager = typeManager;
            _settingsManager = settingsManager;
        }

        /// <summary>
        /// 通知类型列表。
        /// </summary>
        public IEnumerable<NotificationType> Types { get; private set; }

        /// <summary>
        /// 配置。
        /// </summary>
        public NotificationSettings NotificationSettings { get; private set; }

        public void OnGet()
        {
            Types = _typeManager.Fetch();
            NotificationSettings = _settingsManager.GetSettings<NotificationSettings>();
        }

        public IActionResult OnPostSettings(int size)
        {
            NotificationSettings = _settingsManager.GetSettings<NotificationSettings>();
            NotificationSettings.MaxSize = size;
            if (_settingsManager.SaveSettings(NotificationSettings))
            {
                Log("修改了每个用户最大通知数量为：{0}。", size);
                return Success("你已经成功更新了记录数！");
            }

            return Error("更新记录数失败，请重试！");
        }

        public IActionResult OnPostDelete(int[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return Error("请先选择通知类型后在进行删除操作！");
            }

            var types = string.Join(",", _typeManager.Fetch(x => ids.Contains(x.Id))
                .Select(x => x.Name)
                .ToArray());
            var result = _typeManager.Delete(ids);
            Log("删除了通知类型：{0}", types);
            return Json(result, types);
        }
    }
}