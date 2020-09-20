using System.Collections.Generic;
using System.Linq;
using Gentings;
using Gentings.AspNetCore.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Yd.Extensions.Security;

namespace Yd.Extensions.RazorPages.Areas.Security.TagHelpers
{
    /// <summary>
    /// 子用户列表。
    /// </summary>
    [HtmlTargetElement("gt:user-dropdownlist")]
    public class UserDropdownListTagHelper : DropdownListTagHelper
    {
        private readonly IUserManager _userManager;

        public UserDropdownListTagHelper(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 初始化选项列表。
        /// </summary>
        /// <returns>返回选项列表。</returns>
        protected override IEnumerable<SelectListItem> Init()
        {
            return _userManager.LoadUsersByParentId(HttpContext.User.GetUserId())
                .Select(x => new SelectListItem(x.Key, x.Value.ToString()))
                .ToList();
        }
    }
}