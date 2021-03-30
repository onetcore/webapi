using System.Linq;
using System.Threading.Tasks;
using Gentings.Security;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.WebApis.Security.Admin.Roles
{
    /// <summary>
    /// 角色控制器。
    /// </summary>
    public class RolesController : AdminControllerBase
    {
        private readonly IRoleManager _roleManager;
        /// <summary>
        /// 初始化类<see cref="RolesController"/>。
        /// </summary>
        /// <param name="roleManager">角色管理接口。</param>
        public RolesController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// 获取所有角色列表。
        /// </summary>
        /// <returns>所有角色列表。</returns>
        [HttpGet]
        public async Task<IActionResult> Query()
        {
            var roles = await _roleManager.LoadAsync();
            return OkResult(roles.Select(x => new RoleModel(x)));
        }

        /// <summary>
        /// 删除角色。
        /// </summary>
        /// <param name="ids">删除的Id集合。</param>
        /// <returns>返回操作结果。</returns>
        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody]int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return InvalidParameters(nameof(ids));
            var result = await _roleManager.DeleteAsync(ids);
            if (result.Succeeded)
                return OkResult();
            return BadResult(result.ToErrorString());
        }
    }
}