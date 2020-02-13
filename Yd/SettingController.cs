using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions;

namespace Yd
{
    /// <summary>
    /// 配置控制器。
    /// </summary>
    public class SettingController : ApiControllerBase
    {
        /// <summary>
        /// 获取网站配置。
        /// </summary>
        /// <returns>返回网站配置实例。</returns>
        [HttpGet]
        public Task<IActionResult> Index() => GetSettingsAsync<SiteSettings>();
    }
}