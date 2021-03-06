﻿using System.Collections.Generic;
using Gentings.AspNetCore.TagHelpers.Bootstraps;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Yd.Extensions.Security.Roles;

namespace Yd.AspNetCore.TagHelpers
{
    /// <summary>
    /// 角色列表。
    /// </summary>
    [HtmlTargetElement("gt:role-checkboxlist")]
    public class RoleCheckboxListTagHelper : CheckboxListTagHelper
    {
        private readonly IRoleManager _roleManager;
        /// <summary>
        /// 初始化类<see cref="RoleCheckboxListTagHelper"/>。
        /// </summary>
        /// <param name="roleManager">角色管理接口。</param>
        public RoleCheckboxListTagHelper(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// 最高级角色。
        /// </summary>
        [HtmlAttributeName("max")]
        public int MaxRoleLevel { get; set; }

        /// <summary>
        /// 附加复选项目列表，文本/值。
        /// </summary>
        /// <param name="items">复选框项目列表实例。</param>
        protected override void Init(IDictionary<string, object> items)
        {
            foreach (var role in _roleManager.Load())
            {
                if (MaxRoleLevel > 0 && role.RoleLevel >= MaxRoleLevel || role.RoleLevel == 0)
                    continue;
                items.Add(role.Name, role.Id);
            }
        }
    }
}