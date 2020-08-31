using System.Linq;
using Gentings.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.RazorPages
{
    /// <summary>
    /// 页面模型基类。
    /// </summary>
    public abstract class ModelBase : Gentings.Extensions.AspNetCore.ModelBase
    {
        private User _user;
        /// <summary>
        /// 当前登录用户Id。
        /// </summary>
        public new User User => _user ??= HttpContext.GetUser<User>();

        private Role _role;
        /// <summary>
        /// 当前用户的最大角色实例。
        /// </summary>
        public Role Role => _role ??= GetRequiredService<IRoleManager>().GetCacheRole(User.RoleId);

        /// <summary>
        /// 返回JSON试图结果。
        /// </summary>
        /// <param name="result">数据结果。</param>
        /// <returns>返回JSON试图结果。</returns>
        protected IActionResult Error(IdentityResult result)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();
            return Error(string.Join(", ", errors));
        }
    }
}