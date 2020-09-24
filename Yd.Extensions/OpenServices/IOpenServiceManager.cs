using Gentings;
using Gentings.Data;
using Gentings.Extensions;

namespace Yd.Extensions.OpenServices
{
    /// <summary>
    /// 开发服务管理接口。
    /// </summary>
    public interface IOpenServiceManager : IObjectManager<OpenService>, ISingletonService
    {

    }

    /// <summary>
    /// 开发服务管理。
    /// </summary>
    public class OpenServiceManager : ObjectManager<OpenService>, IOpenServiceManager
    {
        /// <summary>
        /// 初始化类<see cref="OpenServiceManager"/>。
        /// </summary>
        /// <param name="context">数据库操作实例。</param>
        public OpenServiceManager(IDbContext<OpenService> context) : base(context)
        {
        }
    }
}