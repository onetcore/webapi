using System.Threading.Tasks;
using Gentings;
using Gentings.Security.Roles;

namespace Yd.Extensions.Security.Roles
{
    /// <summary>
    /// 角色管理。
    /// </summary>
    public interface IRoleManager : IRoleManager<Role, UserRole, RoleClaim>, IScopedService
    {
        /// <summary>
        /// 获取角色验证权限。
        /// </summary>
        /// <param name="roleId">角色Id。</param>
        /// <returns>返回角色验证权限：admin|user|guess。</returns>
        string[] GetAuthority(int roleId);

        /// <summary>
        /// 获取角色验证权限。
        /// </summary>
        /// <param name="roleId">角色Id。</param>
        /// <returns>返回角色验证权限：admin|user|guess。</returns>
        Task<string[]> GetAuthorityAsync(int roleId);
    }
}