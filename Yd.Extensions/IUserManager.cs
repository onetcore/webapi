using System.Collections.Generic;
using System.Threading.Tasks;
using Gentings;
using Gentings.Identity;
using Microsoft.AspNetCore.Http;
using Yd.Extensions.Roles;

namespace Yd.Extensions
{
    /// <summary>
    /// 用户管理接口。
    /// </summary>
    public interface IUserManager : IUserManager<User, Role>, IScopedService
    {
        /// <summary>
        /// 获取缓存用户实例。
        /// </summary>
        /// <param name="id">用户Id。</param>
        /// <returns>返回缓存用户实例对象。</returns>
        CachedUser GetCachedUser(int id);

        /// <summary>
        /// 获取缓存用户实例。
        /// </summary>
        /// <param name="id">用户Id。</param>
        /// <returns>返回缓存用户实例对象。</returns>
        Task<CachedUser> GetCachedUserAsync(int id);

        /// <summary>
        /// 上传头像。
        /// </summary>
        /// <param name="id">用户Id。</param>
        /// <param name="avatarFile">头像文件实例。</param>
        /// <returns>返回上传结果。</returns>
        Task<string> UploadAvatarAsync(int id, IFormFile avatarFile);

        /// <summary>
        /// 获取缓存用户实例列表。
        /// </summary>
        /// <param name="ids">用户Id。</param>
        /// <returns>返回缓存用户实例对象列表。</returns>
        IEnumerable<CachedUser> GetCachedUsers(int[] ids);

        /// <summary>
        /// 获取缓存用户实例列表。
        /// </summary>
        /// <param name="ids">用户Id。</param>
        /// <returns>返回缓存用户实例对象列表。</returns>
        Task<IEnumerable<CachedUser>> GetCachedUsersAsync(int[] ids);
    }
}