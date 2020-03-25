using System.Threading.Tasks;
using Gentings.Identity;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Roles;

namespace Yd.Extensions.Controllers.Admin.Roles
{
    /// <summary>
    /// 角色控制器。
    /// </summary>
    public class RoleController : AdminControllerBase
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
        public async Task<IActionResult> Create([FromBody]CreateRoleModel model)
        {
            var role = new Role
            {
                Name = model.Name,
                Color = model.Color,
                IconUrl = model.IconUrl,
                IsSystem = model.IsSystem,
                IsDefault = model.IsDefault,
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                Log("添加用户角色：{0}", role.Name);
                return OkResult();
            }
            return BadResult(result.ToErrorString());
        }

        /// <summary>
        /// 添加角色。
        /// </summary>
        /// <param name="model">角色模型。</param>
        /// <returns>返回添加结果。</returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]UpdateRoleModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
                return BadResult(ErrorCode.RoleNotFound);
            if (role.Name != model.Name)
            {
                role.Name = model.Name;
                role.NormalizedName = null;
            }

            role.IsSystem = model.IsSystem;
            role.IsDefault = model.IsDefault;
            role.Color = model.Color ?? role.Color;
            role.IconUrl = model.IconUrl ?? role.IconUrl;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                Log("更新用户角色：{0}", role.Name);
                return OkResult();
            }
            return BadResult(result.ToErrorString());
        }
    }
}