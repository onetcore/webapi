using Gentings;
using Gentings.Extensions.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Yd.Extensions
{
    /// <summary>
    /// 注册网站配置。
    /// </summary>
    public class ServiceConfigurer : IServiceConfigurer
    {
        /// <summary>
        /// 配置服务方法。
        /// </summary>
        /// <param name="builder">容器构建实例。</param>
        public void ConfigureServices(IServiceBuilder builder)
        {
            builder.AddScoped(ss => ss.GetRequiredService<ISettingsManager>().GetSettings<SiteSettings>());
        }
    }
}