using System;
using System.Threading.Tasks;
using Gentings.Extensions;
using Gentings.Security;
using Gentings.Security.Permissions;
using Gentings.Security.Scores;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
using Yd.Extensions.Security.Roles;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin
{
    /// <summary>
    /// 用户管理。
    /// </summary>
    [PermissionAuthorize(SecurityPermissions.Users)]
    public class IndexModel : ModelBase
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;

        /// <summary>
        /// 初始化类<see cref="IndexModel"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="roleManager">角色管理接口。</param>
        public IndexModel(IUserManager userManager, IRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 获取角色名称。
        /// </summary>
        /// <param name="roleId">角色Id。</param>
        /// <returns>返回角色Id。</returns>
        public string GetCacheRoleName(int roleId) => _roleManager.GetCacheRole(roleId)?.Name;

        /// <summary>
        /// 查询实例。
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public UserQuery Query { get; set; }

        /// <summary>
        /// 用户列表。
        /// </summary>
        public IPageEnumerable<ViewModel> Model { get; private set; }

        /// <summary>
        /// 用户试图模型。
        /// </summary>
        public class ViewModel
        {
            /// <summary>
            /// 用户Id。
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 用户名称。
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 昵称。
            /// </summary>
            public string NickName { get; set; }

            /// <summary>
            /// 短信数量。
            /// </summary>
            public int Score { get; set; }

            /// <summary>
            /// 电子邮件。
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// 电话号码。
            /// </summary>
            public string PhoneNumber { get; set; }

            /// <summary>
            /// 锁定截止UTC时间。
            /// </summary>
            public DateTimeOffset? LockoutEnd { get; set; }

            /// <summary>
            /// 登录错误达到失败次数，是否锁定账户。
            /// </summary>
            public bool LockoutEnabled { get; set; }

            /// <summary>
            /// 显示角色Id。
            /// </summary>
            public virtual int RoleId { get; set; }

            /// <summary>
            /// 注册时间。
            /// </summary>
            public virtual DateTimeOffset CreatedDate { get; set; }
        }

        /// <summary>
        /// 积分配置。
        /// </summary>
        public ScoreSettings Scores { get; private set; }

        /// <summary>
        /// 获取用户。
        /// </summary>
        public async Task OnGetAsync()
        {
            Query.MaxRoleLevel = Role.RoleLevel;
            Model = await _userManager.LoadAsync<UserQuery, ViewModel>(Query);
            Scores = await GetSettingsAsync<ScoreSettings>();
        }

        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="id">用户Id。</param>
        /// <returns>返回删除结果。</returns>
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var user = await _userManager.FindIndexedUserAsync(id, UserId);
            if (user == null)
                return Error("用户不存在，或者不属于当前用户，不能进行删除操作！");
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                Log($"删除了账户：{user.NickName}({user.UserName})。");
                return Success($"你已经成功删除了账户{user.NickName}!");
            }
            return Error(result.ToErrorString());
        }
    }
}