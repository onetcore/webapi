using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Gentings;
using Gentings.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Gentings.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户管理。
    /// </summary>
    public class UserManager : UserManager<User, Role, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>, IUserManager
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
        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider serviceProvider)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, serviceProvider)
        {
        }

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
            .Select(x => new { x.Id, x.Avatar, x.Email, x.UserName, x.RealName, x.PhoneNumber, x.RoleId })
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
    }
}