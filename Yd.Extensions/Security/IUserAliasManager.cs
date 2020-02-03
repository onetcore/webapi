using System;
using Gentings;
using Gentings.Data;
using Gentings.Extensions;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户别名管理接口。
    /// </summary>
    public interface IUserAliasManager : IObjectManager<UserAlias, Guid>, ISingletonService
    {

    }

    /// <summary>
    /// 用户别名管理类型。
    /// </summary>
    public class UserAliasManager : ObjectManager<UserAlias, Guid>, IUserAliasManager
    {
        /// <summary>
        /// 初始化类<see cref="UserAliasManager"/>。
        /// </summary>
        /// <param name="context">数据库操作实例。</param>
        public UserAliasManager(IDbContext<UserAlias> context) : base(context)
        {
        }
    }
}