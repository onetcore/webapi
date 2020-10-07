using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gentings.Data;
using Gentings.Extensions;
using Gentings.Identity;
using Gentings.Storages.Avatars;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户管理。
    /// </summary>
    public class UserManager : UserManager<User, Role, UserClaim, UserRole, UserLogin, UserToken, RoleClaim, SecuritySettings>, IUserManager
    {
        /// <summary>
        /// 初始化类<see cref="UserManager"/>。
        /// </summary>
        /// <param name="store">用户存储接口。</param>
        /// <param name="optionsAccessor"><see cref="T:Microsoft.AspNetCore.Identity.IdentityOptions" />实例对象。</param>
        /// <param name="passwordHasher">密码加密器接口。</param>
        /// <param name="userValidators">用户验证接口。</param>
        /// <param name="passwordValidators">密码验证接口。</param>
        /// <param name="keyNormalizer">唯一键格式化字符串。</param>
        /// <param name="errors">错误实例。</param>
        /// <param name="serviceProvider">服务提供者接口。</param>
        public UserManager(IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider serviceProvider)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, serviceProvider)
        {
            _scoreDB = serviceProvider.GetRequiredService<IDbContext<UserScore>>();
        }

        private readonly IDbContext<UserScore> _scoreDB;

        /// <summary>
        /// 上传头像。
        /// </summary>
        /// <param name="id">用户Id。</param>
        /// <param name="avatarFile">头像文件实例。</param>
        /// <returns>返回上传结果。</returns>
        public virtual async Task<string> UploadAvatarAsync(int id, IFormFile avatarFile)
        {
            var url = await GetRequiredService<IAvatarManager>().UploadAsync(id, avatarFile);
            if (!string.IsNullOrEmpty(url))
                await UpdateAsync(id, new { Avatar = url });
            return url;
        }

        /// <summary>
        /// 判断消费积分是否足够。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <returns>返回判断结果。</returns>
        public virtual async Task<bool> IsEnoughAsync(int userId, int score)
        {
            var id = await DbContext.UserContext.AsQueryable()
                .WithNolock()
                .Select(x => x.Id)
                .Where(x => x.Id == userId && x.Score > score)
                .FirstOrDefaultAsync(reader => reader.GetInt32(0));
            return id > 0;
        }

        /// <summary>
        /// 充值积分。
        /// </summary>
        /// <param name="sourceId">原始用户Id。</param>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <param name="remark">备注。</param>
        /// <param name="targetId">目标Id。</param>
        /// <returns>返回充值结果。</returns>
        public virtual Task<bool> RechargeAsync(int sourceId, int userId, int score, string remark = null, int? targetId = null)
        {
            return DbContext.UserContext.BeginTransactionAsync(async db =>
           {
               if (await db.UpdateScoreAsync(sourceId, -score, targetId, remark))
                   return await db.UpdateScoreAsync(userId, score, targetId, remark);
               return false;
           });
        }

        /// <summary>
        /// 分页加载用户积分。
        /// </summary>
        /// <param name="query">用户积分查询实例。</param>
        /// <returns>返回用户积分列表。</returns>
        public virtual Task<IPageEnumerable<UserScore>> LoadScoresAsync(UserScoreQuery query)
        {
            return _scoreDB.LoadAsync(query);
        }

        /// <summary>
        /// 更新用户积分。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <param name="score">用户积分。</param>
        /// <param name="remark">描述。</param>
        /// <param name="scoreType">积分使用类型。</param>
        /// <param name="targetId">目标Id。</param>
        /// <returns>返回添加结果。</returns>
        public virtual bool UpdateScore(int userId, int score, string remark = null, ScoreType? scoreType = null, int? targetId = null)
        {
            return DbContext.UserContext.BeginTransaction(db => db.UpdateScore(userId, score, targetId, remark));
        }

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
        public virtual Task<bool> UpdateScoreAsync(int userId, int score, string remark = null, ScoreType? scoreType = null, int? targetId = null, CancellationToken cancellationToken = default)
        {
            return DbContext.UserContext.BeginTransactionAsync(db => db.UpdateScoreAsync(userId, score, targetId, remark, scoreType, cancellationToken), cancellationToken: cancellationToken);
        }
    }
}