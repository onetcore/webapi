using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.Controllers
{
    /// <summary>
    /// 配置控制器。
    /// </summary>
    public class SettingController : ControllerBase
    {
        /// <summary>
        /// 获取网站配置。
        /// </summary>
        /// <returns>返回网站配置实例。</returns>
        [HttpGet]
        public Task<IActionResult> Index() => GetSettingsAsync<SiteSettings>();
    }
}