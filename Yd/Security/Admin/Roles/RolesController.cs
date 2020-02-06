using System.Linq;
using System.Threading.Tasks;
using Gentings.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
using ControllerBase = Gentings.AspNetCore.ControllerBase;

namespace Yd.Security.Admin.Roles
{
    /// <summary>
    /// 角色控制器。
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
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
            return OkResult(roles);
        }

        /// <summary>
        /// 删除角色。
        /// </summary>
        /// <param name="ids">删除的Id集合。</param>
        /// <returns>返回操作结果。</returns>
        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody]int[] ids)
        {
            var roles = await _roleManager.LoadAsync();
            var defaultIds = roles.Where(x =>
                    x.NormalizedName == DefaultRole.MemberName || x.NormalizedName == DefaultRole.OwnerName)
                .Select(x => x.Id)
                .ToArray();
            ids = ids.Where(id => !defaultIds.Contains(id)).ToArray();
            var result = await _roleManager.DeleteAsync(ids);
            if (result.Succeeded)
                return OkResult();
            return BadResult(result.ToErrorString());
        }
    }
}