using System;
using System.Linq;
using System.Threading.Tasks;
using Gentings.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.Controllers.Admin.Users
{
    /// <summary>
    /// 用户管理控制器。
    /// </summary>
    public class UsersController : ApiAdminControllerBase
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// 初始化类<see cref="UsersController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 分页获取用户实例。
        /// </summary>
        /// <param name="query">用户查询实例。</param>
        /// <returns>返回用户分页实例。</returns>
        [HttpGet]
        public async Task<IActionResult> LoadUsers([FromQuery]UserQuery query)
        {
            query.MaxRoleLevel = Role.RoleLevel;
            var data = await _userManager.LoadAsync<UserQuery, UserModel>(query);
            return OkResult(data);
        }

        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="ids">删除用户的Id集合。</param>
        /// <returns>返回删除结果。</returns>
        [HttpPost("remove")]
        public async Task<IActionResult> Remove(int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return BadParameter(nameof(ids));
            if (ids.Contains(UserId))
                return BadResult("不能删除自己的账户！");
            var result = await _userManager.DeleteAsync(ids);
            if (result.Succeeded)
                return OkResult();
            return BadResult(result.ToErrorString());
        }

        /// <summary>
        /// 锁定/结果用户。
        /// </summary>
        /// <param name="model">锁定结果用户。</param>
        /// <returns></returns>
        [HttpPost("lockout")]
        public async Task<IActionResult> Lockout([FromBody] LockoutUserModel model)
        {
            if (model.LockoutEnd < DateTimeOffset.Now)
                return BadParameter(nameof(model.LockoutEnd));

            if (model.Ids == null || model.Ids.Length == 0)
                return BadParameter(nameof(model.Ids));

            if (model.LockoutEnd.Offset == TimeSpan.Zero)
                model.LockoutEnd = model.LockoutEnd.ToOffset(DateTimeOffset.Now.Offset);
            var result = await _userManager.LockoutAsync(model.Ids, model.LockoutEnd);
            if (result)
            {
                Log("锁定了用户：{0} 直到 {1}", string.Join(",", model.Ids), model.LockoutEnd.ToString("yyyy-MM-dd HH:mm:ss"));
                return OkResult();
            }

            return BadResult("锁定失败");
        }

        /// <summary>
        /// 解锁用户。
        /// </summary>
        /// <param name="ids">用户Id。</param>
        /// <returns>返回结果结果。</returns>
        [HttpPost("unlock")]
        public async Task<IActionResult> Unlock([FromBody] int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return BadParameter(nameof(ids));
            var result = await _userManager.LockoutAsync(ids);
            if (result)
            {
                Log("解锁了用户：{0}", string.Join(",", ids));
                return OkResult();
            }

            return BadResult("解锁失败");
        }
    }
}