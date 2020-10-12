using System.Threading.Tasks;
using Gentings.Extensions;
using Gentings.Identity;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Admin
{
    [PermissionAuthorize(SecurityPermissions.Users)]
    public class IndexModel : ModelBase
    {
        private readonly IUserManager _userManager;

        public IndexModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public UserQuery Query { get; set; }

        public IPageEnumerable<Extensions.Security.User> Model { get; private set; }

        public void OnGet()
        {
            Query.MaxRoleLevel = Role.RoleLevel;
            Model = _userManager.Load(Query);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id == UserId)
                return Error("不能删除自己的账号！");
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                Log($"删除了账户{user.UserName}。");
                return Success($"你已经成功删除了账户{user.UserName}!");
            }
            return Error(result.ToErrorString());
        }
    }
}