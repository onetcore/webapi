using System;
using System.Linq.Expressions;
using Gentings.Data;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户列扩展类。
    /// </summary>
    public static class UserFieldExtensions
    {
        /// <summary>
        /// 选择用户相关联字段。
        /// </summary>
        /// <typeparam name="TModel">当前实例模型。</typeparam>
        /// <param name="queryable">查询实例。</param>
        /// <param name="expression">关联表达式。</param>
        /// <returns>返回当前查询实例。</returns>
        public static IQueryable<TModel> JoinSelect<TModel>(this IQueryable<TModel> queryable,
            Expression<Func<TModel, User, bool>> expression)
            where TModel : UserFieldBase
            => queryable
                .WithNolock()
                .InnerJoin<User>(expression)
                .InnerJoin<User, Role>((u, r) => u.RoleId == r.Id)
                .Select<User>(x => new { x.NickName, x.UserName, x.RoleId, x.Avatar })
                .Select<Role>(x => x.Color, "RoleColor")
                .Select<Role>(x => x.Name, "RoleName")
                .Select<Role>(x => x.IconUrl, "RoleIcon");
    }
}