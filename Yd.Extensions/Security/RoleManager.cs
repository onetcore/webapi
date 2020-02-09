using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Gentings.Identity;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 角色管理。
    /// </summary>
    public class RoleManager : RoleManager<Role, UserRole, RoleClaim>, IRoleManager
    {
        /// <summary>
        /// 初始化类<see cref="RoleManager"/>
        /// </summary>
        /// <param name="store">存储接口。</param>
        /// <param name="roleValidators">角色验证集合。</param>
        /// <param name="keyNormalizer">角色唯一键格式化接口。</param>
        /// <param name="errors">错误实例。</param>
        /// <param name="logger">日志接口。</param>
        /// <param name="cache">缓存接口。</param>
        public RoleManager(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger, IMemoryCache cache)
            : base(store, roleValidators, keyNormalizer, errors, logger, cache)
        {
        }

        /// <summary>
        /// 获取第一等级的角色。
        /// </summary>
        /// <param name="roleId">角色Id。</param>
        /// <returns>返回角色实例。</returns>
        public virtual Role GetUnderRole(int roleId)
        {
            var roles = Load().ToList();
            var i = 0;
            for (; i < roles.Count; i++)
            {
                if (roleId == roles[i].Id)
                    break;
            }

            i++;
            if (roles.Count > i++) return roles[i];
            return null;
        }

        /// <summary>
        /// 获取第一等级的角色。
        /// </summary>
        /// <param name="roleId">角色Id。</param>
        /// <returns>返回角色实例。</returns>
        public virtual async Task<Role> GetUnderRoleAsync(int roleId)
        {
            var roles = (await LoadAsync()).ToList();
            var i = 0;
            for (; i < roles.Count; i++)
            {
                if (roleId == roles[i].Id)
                    break;
            }

            i++;
            if (roles.Count > i++) return roles[i];
            return null;
        }
    }
}