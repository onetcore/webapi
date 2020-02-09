using System.Threading.Tasks;
using Gentings;
using Gentings.Identity;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 角色管理。
    /// </summary>
    public interface IRoleManager : IRoleManager<Role, UserRole, RoleClaim>, IScopedService
    {
        /// <summary>
        /// 获取第一等级的角色。
        /// </summary>
        /// <param name="roleId">角色Id。</param>
        /// <returns>返回角色实例。</returns>
        Role GetUnderRole(int roleId);

        /// <summary>
        /// 获取第一等级的角色。
        /// </summary>
        /// <param name="roleId">角色Id。</param>
        /// <returns>返回角色实例。</returns>
        Task<Role> GetUnderRoleAsync(int roleId);
    }
}