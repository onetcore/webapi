using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gentings;
using Gentings.Extensions;
using Gentings.Identity;
using Microsoft.AspNetCore.Http;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security
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

        /// <summary>
        /// 判断消费积分是否足够。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <returns>返回判断结果。</returns>
        Task<bool> IsEnoughAsync(int userId, int score);

        /// <summary>
        /// 充值积分。
        /// </summary>
        /// <param name="sourceId">原始用户Id。</param>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <param name="remark">备注。</param>
        /// <returns>返回充值结果。</returns>
        Task<bool> RechargeAsync(int sourceId, int userId, int score, string remark = null);

        /// <summary>
        /// 分页加载用户积分。
        /// </summary>
        /// <param name="query">用户积分查询实例。</param>
        /// <returns>返回用户积分列表。</returns>
        Task<IPageEnumerable<UserScore>> LoadScoresAsync(UserScoreQuery query);

        /// <summary>
        /// 通过父级Id获取用户名称和Id列表。
        /// </summary>
        /// <param name="parentId">父级用户Id。</param>
        /// <returns>用户名称和Id列表。</returns>
        Dictionary<string, int> LoadUsersByParentId(int parentId);

        /// <summary>
        /// 通过父级Id获取用户名称和Id列表。
        /// </summary>
        /// <param name="parentId">父级用户Id。</param>
        /// <returns>用户名称和Id列表。</returns>
        Task<Dictionary<string, int>> LoadUsersByParentIdAsync(int parentId);

        /// <summary>
        /// 更新用户积分。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <param name="remark">描述。</param>
        /// <param name="scoreType">积分使用类型。</param>
        /// <returns>返回添加结果。</returns>
        bool UpdateScore(int userId, int score, string remark = null, ScoreType? scoreType = null);

        /// <summary>
        /// 更新用户积分。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <param name="remark">描述。</param>
        /// <param name="scoreType">积分使用类型。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回添加结果。</returns>
        Task<bool> UpdateScoreAsync(int userId, int score, string remark = null, ScoreType? scoreType = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取当前用户的所有子账户列表。
        /// </summary>
        /// <param name="userId">当前用户Id。</param>
        /// <returns>返回当前用户的所有子账户列表。</returns>
        Task<IEnumerable<GroupableUser>> LoadSubUsersAsync(int userId);
    }
}