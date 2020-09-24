using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gentings;
using Gentings.AspNetCore.TagHelpers;
using Gentings.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Yd.Extensions.Security;

namespace Yd.Extensions.RazorPages.Areas.Security.TagHelpers
{
    /// <summary>
    /// 所有子用户列表。
    /// </summary>
    [HtmlTargetElement("gt:subuser-dropdownlist")]
    public class SubUserDropdownListTagHelper : DropdownListTagHelper
    {
        private readonly IUserManager _userManager;

        public SubUserDropdownListTagHelper(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 用户Id。
        /// </summary>
        [HtmlAttributeName("userid")]
        public int? UserId { get; set; }

        /// <summary>
        /// 初始化选项列表。
        /// </summary>
        /// <returns>返回选项列表。</returns>
        protected override async Task<IEnumerable<SelectListItem>> InitAsync()
        {
            if (UserId == null)
                UserId = HttpContext.User.GetUserId();
            var users = await _userManager.LoadSubUsersAsync(UserId.Value);
            users = users.MakeDictionary().Values
                .Where(x => x.ParentId == UserId.Value)
                .ToList();
            var items = new List<SelectListItem>();
            InitChildren(items, users);
            return items;
        }
    }
}