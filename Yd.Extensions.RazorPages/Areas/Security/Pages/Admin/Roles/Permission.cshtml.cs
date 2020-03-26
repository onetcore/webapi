using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gentings.Extensions.Settings;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Roles;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Admin.Roles
{
    [PermissionAuthorize(Security.Permissions.Roles)]
    public class PermissionModel : ModelBase
    {
        private readonly IPermissionManager _permissionManager;
        private readonly IRoleManager _roleManager;
        private readonly ISettingDictionaryManager _settingDictionaryManager;

        public PermissionModel(IPermissionManager permissionManager, IRoleManager roleManager, ISettingDictionaryManager settingDictionaryManager)
        {
            _permissionManager = permissionManager;
            _roleManager = roleManager;
            _settingDictionaryManager = settingDictionaryManager;
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

        public string GetDictionaryValue(string key)
        {
            return _settingDictionaryManager.GetOrAddSettings(key);
        }
    }
}