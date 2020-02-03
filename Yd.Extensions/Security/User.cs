using System;
using Gentings.Data.Internal;
using System.Threading;
using System.Threading.Tasks;
using Gentings.Extensions;
using Gentings.Identity;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户。
    /// </summary>
    public class User : UserBase, IUserEventHandler<User>
    {
        #region events
        /// <summary>
        /// 当用户添加后触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public bool OnCreated(IDbTransactionContext<User> context)
        {
            //添加用户角色
            var role = context.As<Role>()
                .Find(x => x.NormalizedName == DefaultRole.Member.NormalizedName);
            var userRole = new UserRole { RoleId = role.Id, UserId = Id };
            if (context.As<UserRole>().Create(userRole))
            {
                //推广链接
                var alias = context.As<UserAlias>();
                for (int i = 0; i < SecuritySettings.AliasCount; i++)
                {
                    alias.Create(new UserAlias { UserId = Id });
                }
                //子账号
                if (ParentId > 0)
                {
                    var tableName = typeof(Subuser).GetTableName();
                    context.ExecuteNonQuery($@"INSERT INTO {tableName}(UserId,SubId)
SELECT UserId, {Id} FROM {tableName} WHERE SubId = {ParentId};");
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 当用户添加后触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public async Task<bool> OnCreatedAsync(IDbTransactionContext<User> context, CancellationToken cancellationToken = default(CancellationToken))
        {
            //添加用户角色
            var role = await context.As<Role>()
                .FindAsync(x => x.NormalizedName == DefaultRole.Member.NormalizedName, cancellationToken);
            var userRole = new UserRole { RoleId = role.Id, UserId = Id };
            if (await context.As<UserRole>().CreateAsync(userRole, cancellationToken))
            {
                var alias = context.As<UserAlias>();
                for (int i = 0; i < SecuritySettings.AliasCount; i++)
                {
                    await alias.CreateAsync(new UserAlias { UserId = Id }, cancellationToken);
                }
                //子账号
                if (ParentId > 0)
                {
                    var tableName = typeof(Subuser).GetTableName();
                    await context.ExecuteNonQueryAsync($@"INSERT INTO {tableName}(UserId,SubId)
SELECT UserId, {Id} FROM {tableName} WHERE SubId = {ParentId};", cancellationToken: cancellationToken);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 当用户更新前触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public bool OnUpdate(IDbTransactionContext<User> context)
        {
            return true;
        }

        /// <summary>
        /// 当用户更新前触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public Task<bool> OnUpdateAsync(IDbTransactionContext<User> context, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// 当用户删除前触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public bool OnDelete(IDbTransactionContext<User> context)
        {
            return true;
        }

        /// <summary>
        /// 当用户删除前触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public Task<bool> OnDeleteAsync(IDbTransactionContext<User> context, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(true);
        }
        #endregion

        /// <summary>
        /// 积分。
        /// </summary>
        [NotUpdated]
        public virtual decimal Score { get; set; }

        /// <summary>
        /// 锁定积分。
        /// </summary>
        [NotUpdated]
        public virtual decimal LockedScore { get; set; }

        /// <summary>
        /// 改变积分时间。
        /// </summary>
        [NotUpdated]
        public virtual DateTimeOffset ScoredDate { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        [Size(64)]
        public virtual string Summary { get; set; }
    }
}