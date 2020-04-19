using System;
using System.Threading.Tasks;

namespace Yd.WebUI.Core
{
    /// <summary>
    /// 网站配置服务实现类。
    /// </summary>
    public class SiteSettingsService : ServiceBase, ISiteSettingsService
    {
        /// <summary>
        /// 获取当前配置实例。
        /// </summary>
        /// <returns>返回网站配置实例。</returns>
        public async Task<SiteSettings> GetSettingsAsync()
        {
            return await GetDataAsync<SiteSettings>("/api/setting") ?? new SiteSettings();
        }

        /// <summary>
        /// 保存网站配置。
        /// </summary>
        /// <param name="settings">配置实例。</param>
        /// <returns>返回配置结果。</returns>
        public Task<ApiResult> SaveSettingsAsync(SiteSettings settings)
        {
            return PostAsync("/api/admin/setting/save", settings);
        }

        /// <summary>
        /// 初始化类<see cref="SiteSettingsService"/>。
        /// </summary>
        /// <param name="serviceProvider">服务提供者接口。</param>
        public SiteSettingsService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}