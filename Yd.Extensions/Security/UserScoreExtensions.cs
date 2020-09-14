using System;
using System.Threading;
using System.Threading.Tasks;
using Gentings.Data.Internal;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户积分扩展类。
    /// </summary>
    public static class UserScoreExtensions
    {
        /// <summary>
        /// 更新用户积分。
        /// </summary>
        /// <param name="db">数据库事务接口实例。</param>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <param name="remark">描述。</param>
        /// <param name="scoreType">积分使用类型。</param>
        /// <returns>返回添加结果。</returns>
        public static bool UpdateScore(this IDbTransactionContext<User> db, int userId, int score, string remark = null, ScoreType? scoreType = null)
        {
            var user = db.Find(userId);
            if (user == null || user.Score < score)
                return false;

            var userScore = new UserScore();
            userScore.BeforeScore = user.Score;
            userScore.Score = -score;
            user.Score -= score;
            user.ScoredDate = DateTimeOffset.Now;
            if (scoreType == null)
                scoreType = score > 0 ? ScoreType.Consume : ScoreType.Recharge;
            userScore.ScoreType = scoreType.Value;
            if (!db.Update(userId, new { user.Score, user.ScoredDate }))
                return false;

            userScore.AfterScore = user.Score;
            userScore.Remark = remark;
            userScore.UserId = userId;

            var sdb = db.As<UserScore>();
            return sdb.Create(userScore);
        }

        /// <summary>
        /// 更新用户积分。
        /// </summary>
        /// <param name="db">数据库事务接口实例。</param>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <param name="remark">描述。</param>
        /// <param name="scoreType">积分使用类型。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回添加结果。</returns>
        public static async Task<bool> UpdateScoreAsync(this IDbTransactionContext<User> db, int userId, int score, string remark = null, ScoreType? scoreType = null, CancellationToken cancellationToken = default)
        {
            var user = await db.FindAsync(userId, cancellationToken);
            if (user == null || user.Score < score)
                return false;

            var userScore = new UserScore();
            userScore.BeforeScore = user.Score;
            userScore.Score = -score;
            user.Score -= score;
            user.ScoredDate = DateTimeOffset.Now;
            if (scoreType == null)
                scoreType = score > 0 ? ScoreType.Consume : ScoreType.Recharge;
            userScore.ScoreType = scoreType.Value;
            if (!await db.UpdateAsync(userId, new { user.Score, user.ScoredDate }, cancellationToken))
                return false;

            userScore.AfterScore = user.Score;
            userScore.Remark = remark;
            userScore.UserId = userId;

            var sdb = db.As<UserScore>();
            return await sdb.CreateAsync(userScore, cancellationToken);
        }
    }
}