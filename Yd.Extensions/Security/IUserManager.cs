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
        /// <param name="targetId">目标Id。</param>
        /// <returns>返回充值结果。</returns>
        Task<bool> RechargeAsync(int sourceId, int userId, int score, string remark = null, int? targetId = null);

        /// <summary>
        /// 分页加载用户积分。
        /// </summary>
        /// <param name="query">用户积分查询实例。</param>
        /// <returns>返回用户积分列表。</returns>
        Task<IPageEnumerable<UserScore>> LoadScoresAsync(UserScoreQuery query);

        /// <summary>
        /// 更新用户积分。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <param name="remark">描述。</param>
        /// <param name="scoreType">积分使用类型。</param>
        /// <param name="targetId">目标Id。</param>
        /// <returns>返回添加结果。</returns>
        bool UpdateScore(int userId, int score, string remark = null, ScoreType? scoreType = null, int? targetId = null);

        /// <summary>
        /// 更新用户积分。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <param name="remark">描述。</param>
        /// <param name="scoreType">积分使用类型。</param>
        /// <param name="targetId">目标Id。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回添加结果。</returns>
        Task<bool> UpdateScoreAsync(int userId, int score, string remark = null, ScoreType? scoreType = null, int? targetId = null, CancellationToken cancellationToken = default);
    }
}