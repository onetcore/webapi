using Gentings;
using Gentings.Identity;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 角色管理。
    /// </summary>
    public interface IRoleManager : IRoleManager<Role, UserRole, RoleClaim>, IScopedService
    {

    }
}