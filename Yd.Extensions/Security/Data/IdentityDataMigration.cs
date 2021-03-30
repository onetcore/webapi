using Gentings.Security.Data;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security.Data
{
    /// <summary>
    /// 数据迁移类。
    /// </summary>
    public class IdentityDataMigration : IdentityDataMigration<User, Role, UserClaim, RoleClaim, UserLogin, UserRole, UserToken>
    {
        /// <summary>
        /// 优先级，在两个迁移数据需要先后时候使用。
        /// </summary>
        public override int Priority => 200;
    }
}