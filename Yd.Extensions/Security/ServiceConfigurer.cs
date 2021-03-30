using Gentings.Security;
using Yd.Extensions.Security.Data;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 注册当前登录用户。
    /// </summary>
    public class ServiceConfigurer : ServiceConfigurer<User, Role, UserRole, UserStore, RoleStore, UserManager, RoleManager, SecuritySettings>
    {
        /// <summary>
        /// 开启的功能模型。
        /// </summary>
        protected override EnabledModule EnabledModule =>
            EnabledModule.PermissionAuthorization | EnabledModule.Notification;
    }
}