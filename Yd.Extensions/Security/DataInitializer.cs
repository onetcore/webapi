using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gentings;
using Gentings.Data;
using Gentings.Data.Initializers;
using Gentings.Data.Internal;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户初始化。
    /// </summary>
    public class DataInitializer : IInitializer
    {
        private readonly IDbContext<User> _context;
        private readonly IUserManager _userManager;
        private readonly ILocalizer _localizer;

        /// <summary>
        /// 初始化类<see cref="DataInitializer"/>。
        /// </summary>
        /// <param name="context">用户数据库操作接口实例。</param>
        /// <param name="userManager">用户管理实例。</param>
        /// <param name="localizer">资源接口。</param>
        public DataInitializer(IDbContext<User> context, IUserManager userManager, ILocalizer localizer)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }

        /// <summary>
        /// 优先级，越大越靠前。
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// 判断是否禁用。
        /// </summary>
        /// <returns>返回判断结果。</returns>
        public Task<bool> IsDisabledAsync()
        {
            return _context.AnyAsync();
        }

        /// <summary>
        /// 安装时候预先执行的接口。
        /// </summary>
        /// <returns>返回执行结果。</returns>
        public Task<bool> ExecuteAsync()
        {
            return _context.BeginTransactionAsync(async db =>
            {
                var roles = new List<Role>();
                var rdb = db.As<Role>();
                foreach (DefaultRoles value in Enum.GetValues(typeof(DefaultRoles)))
                {
                    var role = new Role();
                    roles.Add(role);
                    role.Name = _localizer.GetString(value);
                    role.NormalizedName = role.Name.ToUpper();
                    role.RoleLevel = (int)value;
                    role.IsSystem = true;//系统角色不能删除
                    role.IsDefault = value == DefaultRoles.Members;//默认添加的角色
                    await rdb.CreateAsync(role);
                }

                await CreateAsync(db, "ztang", "ztang.ztang", roles);
                await CreateAsync(db, "ydmin", "ydmin.ydmin", roles.Where(x => x.RoleLevel < (int)DefaultRoles.Developers).ToList());
                return true;
            }, 3000);
        }

        private async Task CreateAsync(IDbTransactionContext<User> db, string userName, string password, List<Role> roles)
        {
            const int score = 100000000;
            var user = new User();
            user.UserName = userName;
            user.PasswordHash = password;
            user.NickName = user.UserName;
            user.Score = score;
            user.NormalizedUserName = _userManager.NormalizeName(user.UserName);
            user.PasswordHash = _userManager.HashPassword(user);
            user.RoleId = roles.OrderByDescending(x => x.RoleLevel).First().Id;
            if (await db.CreateAsync(user))
            {
                var urdb = db.As<UserRole>();
                foreach (var role in roles)
                {
                    await urdb.CreateAsync(new UserRole { RoleId = role.Id, UserId = user.Id });
                }
            }
        }
    }
}