using Gentings.Data.Migrations;
using Gentings.Data.Migrations.Builders;
using Gentings.Identity;

namespace Yd.Extensions.Security
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
        /// 添加用户定义列。
        /// </summary>
        /// <param name="builder">用户表格定义实例。</param>
        protected override void Create(CreateTableBuilder<User> builder)
        {
            base.Create(builder);
            builder.Column(x => x.Score)
                .Column(x => x.LockedScore)
                .Column(x => x.ScoredDate)
                .Column(x => x.Summary);
        }

        /// <summary>
        /// 建立索引。
        /// </summary>
        /// <param name="builder">数据迁移构建实例。</param>
        public override void Up1(MigrationBuilder builder)
        {
            base.Up1(builder);
            builder.CreateTable<Subuser>(table => table
                .Column(x => x.UserId)
                .Column(x => x.SubId)
                .ForeignKey<User>(x => x.UserId, x => x.Id, onDelete: ReferentialAction.Cascade));
            builder.CreateTable<UserScore>(table => table
                .Column(x => x.Id)
                .Column(x => x.UserId)
                .Column(x => x.Score)
                .Column(x => x.BeforeScore)
                .Column(x => x.AfterScore)
                .Column(x => x.SecurityKey)
                .Column(x => x.CreatedDate)
                .ForeignKey<User>(x => x.UserId, x => x.Id, onDelete: ReferentialAction.Cascade));
        }

        /// <summary>
        /// 用户别名，用于推广链接。
        /// </summary>
        public virtual void Up2(MigrationBuilder builder)
        {
            builder.CreateTable<UserAlias>(table => table
                .Column(x=>x.Id)
                .Column(x=>x.UserId)
                .Column(x=>x.Count)
                .ForeignKey<User>(x=>x.UserId, x=>x.Id, onDelete:ReferentialAction.Cascade)
            );
        }
    }
}