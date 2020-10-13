using Gentings.Data.Migrations;
using Gentings.Identity.Data;
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

        /// <summary>
        /// 建立索引。
        /// </summary>
        /// <param name="builder">数据迁移构建实例。</param>
        public override void Up1(MigrationBuilder builder)
        {
            base.Up1(builder);
            builder.CreateTable<UserAlias>(table => table
                .Column(x => x.Id)
                .Column(x => x.UserId)
                .Column(x => x.Count)
                .ForeignKey<User>(x => x.UserId, x => x.Id, onDelete: ReferentialAction.Cascade)
            );
        }
    }
}