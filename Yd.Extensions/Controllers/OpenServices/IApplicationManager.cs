using System;
using System.Threading.Tasks;
using Gentings;
using Gentings.Data;
using Gentings.Extensions;
using Yd.Extensions.Security;

namespace Yd.Extensions.Controllers.OpenServices
{
    /// <summary>
    /// 应用管理接口。
    /// </summary>
    public interface IApplicationManager : IObjectManager<Application, Guid>, ISingletonService
    {
        /// <summary>
        /// 获取用户应用，包含用户实例。
        /// </summary>
        /// <param name="appId">应用Id。</param>
        /// <returns>返回包含用户实例的应用类型实例。</returns>
        Task<Application> FindUserApplicationAsync(Guid appId);
    }

    /// <summary>
    /// 应用管理实现类。
    /// </summary>
    public class ApplicationManager : ObjectManager<Application, Guid>, IApplicationManager
    {
        /// <summary>
        /// 初始化类<see cref="ApplicationManager"/>。
        /// </summary>
        /// <param name="context">数据库操作实例。</param>
        public ApplicationManager(IDbContext<Application> context) : base(context)
        {
        }

        /// <summary>
        /// 获取用户应用，包含用户实例。
        /// </summary>
        /// <param name="appId">应用Id。</param>
        /// <returns>返回包含用户实例的应用类型实例。</returns>
        public virtual Task<Application> FindUserApplicationAsync(Guid appId)
        {
            return Context.AsQueryable().WithNolock()
                .InnerJoin<User>((a, u) => a.UserId == u.Id)
                .Select()//备注，这个需要放在用户之前，否正可能会被覆盖
                .Select<User>(x => x.UserName)
                .Where(x => x.Id == appId)
                .FirstOrDefaultAsync();
        }
    }
}