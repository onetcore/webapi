using System.Collections.Generic;
using System.Linq;
using Gentings.Installers;
using System.Threading.Tasks;
using Gentings.Data;
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

        /// <summary>
        /// 初始化类<see cref="DataInitializer"/>。
        /// </summary>
        /// <param name="context">用户数据库操作接口实例。</param>
        /// <param name="userManager">用户管理实例。</param>
        public DataInitializer(IDbContext<User> context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// 优先级，越大越靠前。
        /// </summary>
        public int Priority { get; }

        private readonly int[] _numbers = { int.MaxValue, 0 };
        private readonly string[] _characters = { "系统管理员", "普通会员" };
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
                for (var i = 0; i < _characters.Length; i++)
                {
                    var role = new Role();
                    roles.Add(role);
                    role.Name = _characters[i];
                    role.NormalizedName = role.Name;
                    role.RoleLevel = _numbers[i];
                    role.IsSystem = i == 0;
                    role.IsDefault = i == 1;
                    await rdb.CreateAsync(role);
                }

                var user = new User();
                user.UserName = "admin";
                user.PasswordHash = "admin";
                user.RealName = user.UserName;
                user.NormalizedUserName = _userManager.NormalizeName(user.UserName);
                user.PasswordHash = _userManager.HashPassword(user);
                user.RoleId = roles.OrderByDescending(x => x.RoleLevel).First().Id;
                if (await db.CreateAsync(user))
                {
                    var urdb = db.As<UserRole>();
                    foreach (var role in roles)
                    {
                        await urdb.CreateAsync(new UserRole {RoleId = role.Id, UserId = user.Id});
                    }
                }

                return true;
            }, 3000);
        }
    }
}