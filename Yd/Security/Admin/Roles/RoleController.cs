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
    public class RoleController : ControllerBase
    {
        private readonly IRoleManager _roleManager;
        /// <summary>
        /// 初始化类<see cref="RoleController"/>。
        /// </summary>
        /// <param name="roleManager">角色管理接口。</param>
        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// 添加角色。
        /// </summary>
        /// <param name="model">角色模型。</param>
        /// <returns>返回添加结果。</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]Role model)
        {
            var result = await _roleManager.CreateAsync(model);
            if (result.Succeeded)
                return OkResult();
            return BadResult(result.ToErrorString());
        }

        /// <summary>
        /// 添加角色。
        /// </summary>
        /// <param name="model">角色模型。</param>
        /// <returns>返回添加结果。</returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]Role model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
                return BadResult(ErrorCode.RoleNotFound);
            if (role.Name != model.Name)
            {
                role.Name = model.Name;
                role.NormalizedName = null;
            }
            role.Color = model.Color ?? role.Color;
            role.IconUrl = model.IconUrl ?? role.IconUrl;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return OkResult();
            return BadResult(result.ToErrorString());
        }
    }
}