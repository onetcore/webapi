using System.Collections.Generic;
using Gentings.Extensions.Emails;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.RazorPages.Areas.Emails.Pages.Admin.Settings
{
    /// <summary>
    /// 邮件配置列表。
    /// </summary>
    [PermissionAuthorize(EmailPermissions.Email)]
    public class IndexModel : ModelBase
    {
        private readonly IEmailSettingsManager _settingsManager;
        /// <summary>
        /// 初始化类<see cref="IndexModel"/>。
        /// </summary>
        /// <param name="settingsManager"></param>
        public IndexModel(IEmailSettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        /// <summary>
        /// 邮件配置列表。
        /// </summary>
        public IEnumerable<EmailSettings> EmailSettings { get; private set; }

        /// <summary>
        /// 获取邮件配置列表。
        /// </summary>
        public void OnGet()
        {
            EmailSettings = _settingsManager.Fetch();
        }

        /// <summary>
        /// 删除邮件配置。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnPostDelete(int id)
        {
            var result = _settingsManager.Delete(id);
            return Json(result, "配置");
        }
    }
}