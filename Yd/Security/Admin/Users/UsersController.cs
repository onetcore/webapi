using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
using ControllerBase = Gentings.AspNetCore.ControllerBase;

namespace Yd.Security.Admin.Users
{
    /// <summary>
    /// 用户管理控制器。
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
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
    }
}