using System.Threading;
using System.Threading.Tasks;
using Gentings.Data.Internal;
using Gentings.Extensions;
using Gentings.Identity.Roles;

namespace Yd.Extensions.Security.Roles
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
            AddUsersToDefaultRole(context);
            return true;
        }

        /// <summary>
        /// 当角色更新后触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public async Task<bool> OnCreatedAsync(IDbTransactionContext<Role> context, CancellationToken cancellationToken = default)
        {
            await AddUsersToDefaultRoleAsync(context, cancellationToken);
            return true;
        }

        /// <summary>
        /// 如果当前角色是默认角色，将所有用户添加到角色中。
        /// </summary>
        /// <param name="context">当前事务实例。</param>
        protected virtual void AddUsersToDefaultRole(IDbTransactionContext<Role> context)
        {
            if (!IsDefault) return;
            var urdb = context.As<UserRole>();
            urdb.Delete(x => x.RoleId == Id);
            var tableName = typeof(User).GetTableName();
            context.ExecuteNonQuery(GetAddUsersToRoleSQL(urdb.EntityType.Table, tableName));
        }

        /// <summary>
        /// 如果当前角色是默认角色，将所有用户添加到角色中。
        /// </summary>
        /// <param name="context">当前事务实例。</param>
        /// <param name="cancellationToken">取消标识。</param>
        protected virtual async Task AddUsersToDefaultRoleAsync(IDbTransactionContext<Role> context, CancellationToken cancellationToken)
        {
            if (!IsDefault) return;
            var urdb = context.As<UserRole>();
            await urdb.DeleteAsync(x => x.RoleId == Id, cancellationToken);
            var tableName = typeof(User).GetTableName();
            await context.ExecuteNonQueryAsync(GetAddUsersToRoleSQL(urdb.EntityType.Table, tableName), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 获取将用户添加到角色中的SQL语句。
        /// </summary>
        /// <param name="userRoleTable">用户角色关联表格。</param>
        /// <param name="userTable">用户表格。</param>
        /// <returns>返回SQL语句。</returns>
        protected virtual string GetAddUsersToRoleSQL(string userRoleTable, string userTable)
        {
            return $"INSERT INTO {userRoleTable}(UserId, RoleId) SELECT Id, {Id} FROM {userTable}";
        }

        /// <summary>
        /// 当角色更新前触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public bool OnUpdate(IDbTransactionContext<Role> context)
        {
            //更改用户显示的角色名称
            AddUsersToDefaultRole(context);
            return true;
        }

        /// <summary>
        /// 当角色更新前触发得方法。
        /// </summary>
        /// <param name="context">数据库事务操作实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回操作结果，返回<c>true</c>表示操作成功，将自动提交事务，如果<c>false</c>或发生错误，则回滚事务。</returns>
        public async Task<bool> OnUpdateAsync(IDbTransactionContext<Role> context, CancellationToken cancellationToken = default)
        {
            //更改用户显示的角色名称
            await AddUsersToDefaultRoleAsync(context, cancellationToken);
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
        public Task<bool> OnDeleteAsync(IDbTransactionContext<Role> context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }
    }
}