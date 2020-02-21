using System.Threading;
using System.Threading.Tasks;
using Gentings;
using Gentings.Data;
using Gentings.Extensions;

namespace Yd.Extensions
{
    /// <summary>
    /// 用户别名管理接口。
    /// </summary>
    public interface IUserAliasManager : IObjectManager<UserAlias, string>, ISingletonService
    {

    }

    /// <summary>
    /// 用户别名管理类型。
    /// </summary>
    public class UserAliasManager : ObjectManager<UserAlias, string>, IUserAliasManager
    {
        /// <summary>
        /// 初始化类<see cref="UserAliasManager"/>。
        /// </summary>
        /// <param name="context">数据库操作实例。</param>
        public UserAliasManager(IDbContext<UserAlias> context) : base(context)
        {
        }

        /// <summary>
        /// 通过唯一键获取当前值。
        /// </summary>
        /// <param name="id">唯一Id。</param>
        /// <returns>返回当前模型实例。</returns>
        public override UserAlias Find(string id)
        {
            return AsQueryable()
                .InnerJoin<User>((a, u) => a.UserId == u.Id)
                .WithNolock()
                .Where(x => x.Id == id)
                .Select()
                .Select<User>(x => new { x.Level })
                .FirstOrDefault();
        }

        /// <summary>
        /// 通过唯一键获取当前值。
        /// </summary>
        /// <param name="id">唯一Id。</param>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回当前模型实例。</returns>
        public override Task<UserAlias> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            return AsQueryable()
                .InnerJoin<User>((a, u) => a.UserId == u.Id)
                .WithNolock()
                .Where(x => x.Id == id)
                .Select()
                .Select<User>(x => new { x.Level })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}