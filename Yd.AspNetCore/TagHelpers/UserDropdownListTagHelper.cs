using System.Collections.Generic;
using System.Threading.Tasks;
using Gentings;
using Gentings.AspNetCore.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Yd.Extensions.Security;

namespace Yd.AspNetCore.TagHelpers
{
    /// <summary>
    /// 所有子用户列表。
    /// </summary>
    [HtmlTargetElement("gt:user-dropdownlist")]
    public class UserDropdownListTagHelper : DropdownListTagHelper
    {
        private readonly IUserManager _userManager;
        /// <summary>
        /// 初始化类<see cref="UserDropdownListTagHelper"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口实例。</param>
        public UserDropdownListTagHelper(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 用户Id。
        /// </summary>
        [HtmlAttributeName("userid")]
        public int? UserId { get; set; }

        /// <summary>
        /// 是否只是第一子集。
        /// </summary>
        [HtmlAttributeName("toppest")]
        public bool Toppest { get; set; }

        /// <summary>
        /// 初始化选项列表。
        /// </summary>
        /// <returns>返回选项列表。</returns>
        protected override async Task<IEnumerable<SelectListItem>> InitAsync()
        {
            if (UserId == null)
                UserId = HttpContext.User.GetUserId();
            var users = await _userManager.LoadChildrenAsync(UserId.Value, Toppest);
            var items = new List<SelectListItem>();
            InitChildren(items, users);
            return items;
        }
    }
}