using Gentings;
using Gentings.Identity;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户管理接口。
    /// </summary>
    public interface IUserManager : IUserManager<User, Role>, IScopedService
    {
    }
}