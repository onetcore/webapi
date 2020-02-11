using System.Threading;
using System.Threading.Tasks;
using Gentings.Data.Internal;
using Gentings.Extensions;
using Gentings.Identity;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 角色。
    /// </summary>
    public class Role : RoleBase, IRoleEventHandler<Role>
    {
        /// <summary>
        /// 当角色添加后触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public bool OnCreated(IDbTransactionContext<Role> context)
        {
            if (IsDefault)
                AddUserRole(context);
            return true;
        }

        /// <summary>
        /// 当角色更新后触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public async Task<bool> OnCreatedAsync(IDbTransactionContext<Role> context, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (IsDefault)
                await AddUserRoleAsync(context, cancellationToken);
            return true;
        }

        private void AddUserRole(IDbTransactionContext<Role> context)
        {
            var urdb = context.As<UserRole>();
            urdb.Delete(x => x.RoleId == Id);
            var tableName = typeof(User).GetTableName();
            context.ExecuteNonQuery(
                $"INSERT INTO {urdb.EntityType.Table}(UserId, RoleId) SELECT Id, {Id} FROM {tableName}");
        }

        private async Task AddUserRoleAsync(IDbTransactionContext<Role> context, CancellationToken cancellationToken)
        {
            var urdb = context.As<UserRole>();
            await urdb.DeleteAsync(x => x.RoleId == Id, cancellationToken);
            var tableName = typeof(User).GetTableName();
            await context.ExecuteNonQueryAsync($"INSERT INTO {urdb.EntityType.Table}(UserId, RoleId) SELECT Id, {Id} FROM {tableName}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 当角色更新前触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public bool OnUpdate(IDbTransactionContext<Role> context)
        {
            //更改用户显示的角色名称
            var role = context.Find(Id);
            if (IsDefault && role.IsDefault != IsDefault)
                AddUserRole(context);
            return true;
        }

        /// <summary>
        /// 当角色更新前触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public async Task<bool> OnUpdateAsync(IDbTransactionContext<Role> context, CancellationToken cancellationToken = default(CancellationToken))
        {
            //更改用户显示的角色名称
            var role = context.Find(Id);
            if (IsDefault && role.IsDefault != IsDefault)
                await AddUserRoleAsync(context, cancellationToken);
            return true;
        }

        /// <summary>
        /// 当角色删除前触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public bool OnDelete(IDbTransactionContext<Role> context)
        {
            return true;
        }

        /// <summary>
        /// 当角色删除前触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public Task<bool> OnDeleteAsync(IDbTransactionContext<Role> context, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(true);
        }
    }
}