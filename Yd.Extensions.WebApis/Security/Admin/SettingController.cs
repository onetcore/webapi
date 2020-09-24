using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.WebApis.Security.Admin
{
    /// <summary>
    /// 配置控制器。
    /// </summary>
    public class SettingController : AdminControllerBase
    {
        /// <summary>
        /// 保存网站配置。
        /// </summary>
        /// <param name="settings">网站配置实例。</param>
        /// <returns>返回保存结果。</returns>
        [Authorize]
        [HttpPost("save")]
        public Task<IActionResult> Save(SiteSettings settings) => SaveSettingsAsync(settings, "网站");
    }
}