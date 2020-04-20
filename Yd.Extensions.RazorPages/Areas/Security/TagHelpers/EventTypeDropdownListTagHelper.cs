using System.Collections.Generic;
using System.Linq;
using Gentings.AspNetCore.RazorPages.TagHelpers;
using Gentings.Extensions.AspNetCore.EventLogging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Yd.Extensions.RazorPages.Areas.Security.TagHelpers
{
    /// <summary>
    /// 事件类型。
    /// </summary>
    [HtmlTargetElement("gt:event-type-dropdownlist")]
    public class EventTypeDropdownListTagHelper : DropdownListTagHelper
    {
        private readonly IEventTypeManager _eventManager;

        public EventTypeDropdownListTagHelper(IEventTypeManager eventManager)
        {
            _eventManager = eventManager;
        }

        /// <summary>
        /// 初始化选项列表。
        /// </summary>
        /// <returns>返回选项列表。</returns>
        protected override IEnumerable<SelectListItem> Init()
        {
            return _eventManager.Fetch().Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }
    }
}