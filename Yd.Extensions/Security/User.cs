using System;
using System.Linq;
using Gentings.Data.Internal;
using System.Threading;
using System.Threading.Tasks;
using Gentings.Extensions;
using Gentings.Identity;
using Yd.Extensions.Security.Roles;

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
            try
            {
                //添加用户角色
                var roles = context.As<Role>().Fetch(x => x.IsDefault).ToList();
                var urdb = context.As<UserRole>();
                foreach (var role in roles)
                {
                    var userRole = new UserRole { RoleId = role.Id, UserId = Id };
                    urdb.Create(userRole);
                }
                //更新用户最大角色，用于显示等使用
                var maxRole = roles.OrderByDescending(x => x.RoleLevel).First();
                if (maxRole != null)
                {
                    RoleId = maxRole.Id;
                    context.Update(Id, new { RoleId });
                }
                //推广链接
                var alias = context.As<UserAlias>();
                for (int i = 0; i < SiteSettings.AliasCount; i++)
                {
                    alias.Create(new UserAlias { UserId = Id });
                }
                //子账号
                if (ParentId > 0)
                {
                    var sdb = context.As<Subuser>();
                    context.ExecuteNonQuery($@"INSERT INTO {sdb.EntityType.Table}(UserId,SubId)
SELECT UserId, {Id} FROM {sdb.EntityType.Table} WHERE SubId = {ParentId};");
                    sdb.Create(new Subuser { SubId = Id, UserId = ParentId });
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 当用户添加后触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public async Task<bool> OnCreatedAsync(IDbTransactionContext<User> context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                //添加用户角色
                var roles = await context.As<Role>().FetchAsync(x => x.IsDefault, cancellationToken);
                var urdb = context.As<UserRole>();
                foreach (var role in roles)
                {
                    var userRole = new UserRole { RoleId = role.Id, UserId = Id };
                    await urdb.CreateAsync(userRole, cancellationToken);
                }
                //更新用户最大角色，用于显示等使用
                var maxRole = roles.OrderByDescending(x => x.RoleLevel).First();
                if (maxRole != null)
                {
                    RoleId = maxRole.Id;
                    await context.UpdateAsync(Id, new { RoleId }, cancellationToken);
                }
                //推广链接
                var alias = context.As<UserAlias>();
                for (int i = 0; i < SiteSettings.AliasCount; i++)
                {
                    await alias.CreateAsync(new UserAlias { UserId = Id }, cancellationToken);
                }
                //子账号
                if (ParentId > 0)
                {
                    var sdb = context.As<Subuser>();
                    await context.ExecuteNonQueryAsync($@"INSERT INTO {sdb.EntityType.Table}(UserId,SubId)
SELECT UserId, {Id} FROM {sdb.EntityType.Table} WHERE SubId = {ParentId};", cancellationToken: cancellationToken);
                    await sdb.CreateAsync(new Subuser {SubId = Id, UserId = ParentId}, cancellationToken);
                }

                return true;
            }
            catch
            {
                return false;
            }
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
        /// 用户等级。
        /// </summary>
        [NotUpdated]
        public virtual int Level { get; set; }

        /// <summary>
        /// 用户类型。
        /// </summary>
        [NotUpdated]
        public virtual UserType Type { get; set; }

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