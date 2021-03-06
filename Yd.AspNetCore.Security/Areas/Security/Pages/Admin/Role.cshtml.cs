﻿using System.Linq;
using System.Threading.Tasks;
using Gentings.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
using Yd.Extensions.Security.Roles;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin
{
    /// <summary>
    /// 角色模型。
    /// </summary>
    [PermissionAuthorize(SecurityPermissions.Users)]
    public class RoleModel : ModelBase
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;

        public RoleModel(IUserManager userManager, IRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 试图模型。
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// 角色Id。
            /// </summary>
            public int[] RoleId { get; set; }

            /// <summary>
            /// 用户Id。
            /// </summary>
            public int UserId { get; set; }
        }

        /// <summary>
        /// 用户名称。
        /// </summary>
        public string Name { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("未找到用户！");
            Input = new InputModel
            {
                UserId = user.Id,
                RoleId = await _userManager.GetRoleIdsAsync(id)
            };
            Name = user.UserName;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var roles = (await _roleManager.LoadAsync()).Where(x => x.IsDefault).Select(x => x.Id).ToArray();
            if (Input.RoleId == null)
                Input.RoleId = roles;
            else
                Input.RoleId = Input.RoleId.Concat(roles).Distinct().ToArray();
            var user = await _userManager.FindByIdAsync(Input.UserId);
            var role = await _roleManager.FindByIdAsync(user.RoleId);
            if (role > Role) //没有权限
                return Error("不能设置比自己等级高的用户角色！");
            if (await _userManager.SetUserToRolesAsync(user.Id, Input.RoleId))
            {
                var roleNames = await _userManager.GetRolesAsync(user);
                Log($"设置了用户“{user.UserName}”的角色为：{string.Join(",", roleNames)}。");
                return Success("你已经成功设置了用户的角色！");
            }
            return Error("设置用户角色失败，请重试！");
        }
    }
}