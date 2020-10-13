using Gentings;
using Gentings.Identity;
using Microsoft.Extensions.DependencyInjection;
using Yd.Extensions.Security.Data;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 注册当前登录用户。
    /// </summary>
    public class ServiceConfigurer : ServiceConfigurer<User, Role, UserStore, RoleStore, UserManager, RoleManager>
    {
        /// <summary>
        /// 配置服务。
        /// </summary>
        /// <param name="builder">容器构建实例。</param>
        protected override void ConfigureIdentityServices(IServiceBuilder builder)
        {
            builder.AddScoped(service => service.GetRequiredService<IUserManager>().GetUser() ?? _anonymous);
        }

        private static readonly User _anonymous = new User { UserName = "Anonymous" };
    }
}