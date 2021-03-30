using Gentings;
using Gentings.AspNetCore;

namespace Yd.AspNetCore.Security
{
    /// <summary>
    /// 服务配置。
    /// </summary>
    [Suppress(typeof(Extensions.Security.ServiceConfigurer))]
    public class ServiceConfigurer : Extensions.Security.ServiceConfigurer
    {
        /// <summary>
        /// 配置服务方法。
        /// </summary>
        /// <param name="builder">容器构建实例。</param>
        public override void ConfigureServices(IServiceBuilder builder)
        {
            base.ConfigureServices(builder);
            builder.AddResources<ServiceConfigurer>();
        }
    }
}