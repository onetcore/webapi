using System.Threading;
using System.Threading.Tasks;
using Gentings.Data.Internal;
using Gentings.Identity;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户事件。
    /// </summary>
    public class UserEventHandler : UserEventHandler<User, Role, UserRole>
    {
        /// <summary>
        /// 当用户添加后触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="user">用户实例。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public override bool OnCreated(IDbTransactionContext<User> context, User user)
        {
            base.OnCreated(context, user);
            //别名推广链接
            var alias = context.As<UserAlias>();
            for (int i = 0; i < SiteSettings.AliasCount; i++)
            {
                alias.Create(new UserAlias { UserId = user.Id });
            }

            return true;
        }

        /// <summary>
        /// 当用户添加后触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="user">用户实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public override async Task<bool> OnCreatedAsync(IDbTransactionContext<User> context, User user, CancellationToken cancellationToken = default)
        {
            await base.OnCreatedAsync(context, user, cancellationToken);
            //别名推广链接
            var alias = context.As<UserAlias>();
            for (int i = 0; i < SiteSettings.AliasCount; i++)
            {
                await alias.CreateAsync(new UserAlias { UserId = user.Id }, cancellationToken);
            }

            return true;
        }
    }
}