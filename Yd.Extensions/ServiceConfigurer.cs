using Gentings;
using Gentings.Data.SqlServer;
using Gentings.Tasks;

namespace Yd.Extensions
{
    /// <summary>
    /// 添加服务注册。
    /// </summary>
    public class ServiceConfigurer : IServiceConfigurer
    {
        /// <summary>
        /// 配置服务方法。
        /// </summary>
        /// <param name="builder">容器构建实例。</param>
        public void ConfigureServices(IServiceBuilder builder)
        {
            builder.AddSqlServer()//添加SQLServer数据库
                .AddTaskServices()//添加后台服务。
                ;
        }
    }
}