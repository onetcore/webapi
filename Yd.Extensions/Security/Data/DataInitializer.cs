using System;
using Gentings.Identity.Data;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security.Data
{
    /// <summary>
    /// 用户初始化。
    /// </summary>
    public class DataInitializer : DataInitializer<User, Role, UserRole>
    {
        /// <summary>
        /// 初始化类<see cref="DataInitializer"/>。
        /// </summary>
        /// <param name="serviceProvider">服务提供者接口。</param>
        /// <param name="userManager">用户管理接口。</param>
        public DataInitializer(IServiceProvider serviceProvider, IUserManager userManager)
            : base(serviceProvider, userManager)
        {
        }

        /// <summary>
        /// 默认角色类型。
        /// </summary>
        protected override Type DefaultRolesType { get; } = typeof(DefaultRoles);

        /// <summary>
        /// 判断哪些默认角色为默认添加到用户的角色，如果返回<c>true</c>，则添加用户时候会自动添加到用户中。
        /// </summary>
        /// <param name="defaultRole">默认角色枚举实例。</param>
        /// <returns>返回判断结果。</returns>
        protected override bool IsDefault(Enum defaultRole)
        {
            return (DefaultRoles)defaultRole == DefaultRoles.Members;
        }
    }
}