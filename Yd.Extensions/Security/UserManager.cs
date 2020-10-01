using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gentings;
using Gentings.Data;
using Gentings.Extensions;
using Gentings.Identity;
using Gentings.Storages.Avatars;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly Type _cacheKey = typeof(CachedUser);
        private readonly IEntityType _cachedUser = typeof(CachedUser).GetEntityType();

        /// <summary>
        /// 获取缓存用户实例。
        /// </summary>
        /// <param name="id">用户Id。</param>
        /// <returns>返回缓存用户实例对象。</returns>
        public virtual CachedUser GetCachedUser(int id)
        {
            if (CachedUsers.TryGetValue(id, out var user))
                return user;

            user = CachedQueryable.Where(x => x.Id == id).FirstOrDefault(Converter);
            CachedUsers.TryAdd(id, user);
            return user;
        }

        /// <summary>
        /// 缓存用户实例。
        /// </summary>
        protected ConcurrentDictionary<int, CachedUser> CachedUsers
            => Cache.GetOrCreate(_cacheKey, ctx =>
            {
                ctx.SetDefaultAbsoluteExpiration();
                return new ConcurrentDictionary<int, CachedUser>();
            });

        /// <summary>
        /// 缓存查询实例。
        /// </summary>
        protected virtual Gentings.Data.IQueryable<User> CachedQueryable => DbContext.UserContext.AsQueryable()
            .WithNolock()
            .InnerJoin<Role>((u, r) => u.RoleId == r.Id)
            .Select(x => new { x.Id, x.Avatar, x.Email, x.UserName, x.NickName, x.PhoneNumber, x.RoleId })
            .Select<Role>(x => x.Color, "RoleColor")
            .Select<Role>(x => x.Name, "RoleName")
            .Select<Role>(x => x.IconUrl, "RoleIcon")
            .Select<Role>(x => x.RoleLevel);

        /// <summary>
        /// 获取缓存用户实例。
        /// </summary>
        /// <param name="id">用户Id。</param>
        /// <returns>返回缓存用户实例对象。</returns>
        public virtual async Task<CachedUser> GetCachedUserAsync(int id)
        {
            if (CachedUsers.TryGetValue(id, out var user))
                return user;

            user = await CachedQueryable.Where(x => x.Id == id).FirstOrDefaultAsync(Converter);
            CachedUsers.TryAdd(id, user);
            return user;
        }

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
        /// 转换实例。
        /// </summary>
        /// <param name="reader">数据库读取器。</param>
        /// <returns>返回缓存用户实例。</returns>
        protected CachedUser Converter(DbDataReader reader) => _cachedUser.Read<CachedUser>(reader);

        /// <summary>
        /// 获取缓存用户实例列表。
        /// </summary>
        /// <param name="ids">用户Id。</param>
        /// <returns>返回缓存用户实例对象列表。</returns>
        public virtual IEnumerable<CachedUser> GetCachedUsers(int[] ids)
        {
            var users = CachedUsers.Values.Where(x => ids.Contains(x.Id)).ToList();
            var cacheds = users.Select(x => x.Id);
            ids = ids.Where(x => !cacheds.Contains(x)).ToArray();
            if (ids.Length > 0)
            {
                var current = CachedQueryable.Where(x => x.Included(ids)).AsEnumerable(Converter);
                foreach (var cachedUser in current)
                {
                    CachedUsers.TryAdd(cachedUser.Id, cachedUser);
                }
                users.AddRange(current);
            }

            return users;
        }

        /// <summary>
        /// 获取缓存用户实例列表。
        /// </summary>
        /// <param name="ids">用户Id。</param>
        /// <returns>返回缓存用户实例对象列表。</returns>
        public virtual async Task<IEnumerable<CachedUser>> GetCachedUsersAsync(int[] ids)
        {
            var users = CachedUsers.Values.Where(x => ids.Contains(x.Id)).ToList();
            var cacheds = users.Select(x => x.Id);
            ids = ids.Where(x => !cacheds.Contains(x)).ToArray();
            if (ids.Length > 0)
            {
                var current = await CachedQueryable.Where(x => x.Included(ids)).AsEnumerableAsync(Converter);
                foreach (var cachedUser in current)
                {
                    CachedUsers.TryAdd(cachedUser.Id, cachedUser);
                }
                users.AddRange(current);
            }

            return users;
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
        /// 通过父级Id获取用户名称和Id列表。
        /// </summary>
        /// <param name="parentId">父级用户Id。</param>
        /// <returns>用户名称和Id列表。</returns>
        public virtual Dictionary<string, int> LoadUsersByParentId(int parentId)
        {
            return DbContext.UserContext.AsQueryable()
                .Select(x => new { x.Id, x.NickName })
                .Where(x => x.ParentId == parentId)
                .AsEnumerable(reader => new
                {
                    Id = reader.GetInt32(0),
                    NickName = reader.GetString(1)
                })
                .ToDictionary(x => x.NickName, x => x.Id);
        }

        /// <summary>
        /// 通过父级Id获取用户名称和Id列表。
        /// </summary>
        /// <param name="parentId">父级用户Id。</param>
        /// <returns>用户名称和Id列表。</returns>
        public virtual async Task<Dictionary<string, int>> LoadUsersByParentIdAsync(int parentId)
        {
            var users = await DbContext.UserContext.AsQueryable()
                .Select(x => new { x.Id, x.NickName })
                .Where(x => x.ParentId == parentId)
                .AsEnumerableAsync(reader => new
                {
                    Id = reader.GetInt32(0),
                    NickName = reader.GetString(1)
                });
            return users.ToDictionary(x => x.NickName, x => x.Id);
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

        /// <summary>
        /// 获取当前用户的所有子账户列表。
        /// </summary>
        /// <param name="userId">当前用户Id。</param>
        /// <returns>返回当前用户的所有子账户列表。</returns>
        public virtual Task<IEnumerable<GroupableUser>> LoadSubUsersAsync(int userId)
        {
            return DbContext.UserContext.AsQueryable().WithNolock()
                .InnerJoin<Subuser>((u, s) => u.Id == s.SubId)
                .Where<Subuser>(x => x.UserId == userId)
                .Select(x => new { x.Id, x.ParentId })
                .Select<User>(x => x.NickName, "Name")
                .AsEnumerableAsync<GroupableUser>();
        }
    }
}