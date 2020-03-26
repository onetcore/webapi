﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gentings.Identity;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Roles;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Admin.Roles
{
    /// <summary>
    /// 角色。
    /// </summary>
    [PermissionAuthorize(Security.Permissions.Roles)]
    public class IndexModel : ModelBase
    {
        private readonly IRoleManager _roleManager;

        public IndexModel(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public List<Role> Roles { get; private set; }

        public void OnGet()
        {
            Roles = _roleManager.Load().Where(x => x.RoleLevel <= Role.RoleLevel).OrderByDescending(x => x.RoleLevel).ToList();
        }

        public async Task<IActionResult> OnPostMoveUpAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (await _roleManager.MoveUpAsync(role))
            {
                Log($"上移角色“{role.Name}”！");
                return Success();
            }
            return Error($"上移角色“{role.Name}”失败！");
        }

        public async Task<IActionResult> OnPostMoveDownAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (await _roleManager.MoveDownAsync(role))
            {
                Log($"下移角色“{role.Name}”！");
                return Success();
            }
            return Error($"下移角色“{role.Name}”失败！");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                Log($"删除角色“{role.Name}”！");
                return Success("恭喜你，你已经成功删除角色");
            }
            return Error(result.ToErrorString());
        }
    }
}