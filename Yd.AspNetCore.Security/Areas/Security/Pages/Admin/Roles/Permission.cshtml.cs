using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gentings.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security.Roles;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.Roles
{
    /// <summary>
    /// 角色权限列表。
    /// </summary>
    [PermissionAuthorize(SecurityPermissions.Roles)]
    public class PermissionModel : ModelBase
    {
        private readonly IPermissionManager _permissionManager;
        private readonly IRoleManager _roleManager;

        public PermissionModel(IPermissionManager permissionManager, IRoleManager roleManager)
        {
            _permissionManager = permissionManager;
            _roleManager = roleManager;
        }

        public Role Current { get; private set; }
        
        public IDictionary<string, List<Permission>> Permissions { get; private set; }

        public async Task OnGetAsync(int id)
        {
            Current = await _roleManager.FindByIdAsync(id);
            var permissions = await _permissionManager.LoadPermissionsAsync();
            Permissions = permissions
                .Where(x => _permissionManager.IsAuthorized(x.Key))
                .GroupBy(x => x.Category)
                .ToDictionary(x => x.Key, x => x.ToList());
        }

        public async Task<IActionResult> OnPostAsync(int roleId)
        {
            var result = await _permissionManager.SaveAsync(roleId, Request);
            if (result.Succeed())
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                Log($"设置了“{role.Name}”的权限。");
            }
            return Json(result, "权限");
        }
    }
}