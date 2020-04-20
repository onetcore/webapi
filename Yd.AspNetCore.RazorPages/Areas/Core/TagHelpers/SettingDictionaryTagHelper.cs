using System.Collections.Generic;
using Gentings.AspNetCore.RazorPages.TagHelpers;
using Gentings.Extensions.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Yd.AspNetCore.RazorPages.Areas.Core.TagHelpers
{
    /// <summary>
    /// 字典配置下拉列表框。
    /// </summary>
    [HtmlTargetElement("gt:settings-dictionary-dropdownlist")]
    public class SettingDictionaryTagHelper : DropdownListTagHelper
    {
        private readonly ISettingDictionaryManager _settingManager;

        public SettingDictionaryTagHelper(ISettingDictionaryManager settingManager)
        {
            _settingManager = settingManager;
        }

        /// <summary>
        /// 当前实例Id。
        /// </summary>
        [HtmlAttributeName("current")]
        public int Current { get; set; }

        /// <summary>
        /// 初始化选项列表。
        /// </summary>
        /// <returns>返回选项列表。</returns>
        protected override IEnumerable<SelectListItem> Init()
        {
            var current = _settingManager.Find(Current);
            if (current.Parent != null)
            {
                foreach (var setting in current.Parent.Children)
                {
                    yield return new SelectListItem(setting.Value, setting.Id.ToString());
                }
            }
        }
    }
}