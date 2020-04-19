using System.Threading.Tasks;
using Gentings;

namespace Yd.WebUI.Core
{
    /// <summary>
    /// 配置服务接口。
    /// </summary>
    public interface ISiteSettingsService : IServiceBase, IScopedService
    {
        /// <summary>
        /// 获取当前配置实例。
        /// </summary>
        /// <returns>返回网站配置实例。</returns>
        Task<SiteSettings> GetSettingsAsync();

        /// <summary>
        /// 保存网站配置。
        /// </summary>
        /// <param name="settings">配置实例。</param>
        /// <returns>返回配置结果。</returns>
        Task<ApiResult> SaveSettingsAsync(SiteSettings settings);
    }
}