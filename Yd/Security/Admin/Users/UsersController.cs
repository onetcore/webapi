using System.Linq;
using System.Threading.Tasks;
using Gentings.Identity;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.Security.Admin.Users
{
    /// <summary>
    /// 用户管理控制器。
    /// </summary>
    public class UsersController : ControllerBase
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
    }
}