﻿using System;
using Gentings.AspNetCore.AdminMenus;
using Gentings.Extensions.Settings;
using Microsoft.Extensions.DependencyInjection;
using Yd.Extensions.Security;

namespace Yd.Extensions.RazorPages.Areas.Core
{
    /// <summary>
    /// 管理菜单。
    /// </summary>
    public class AdminMenuProvider : MenuProvider
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 初始化类<see cref="AdminMenuProvider"/>。
        /// </summary>
        /// <param name="serviceProvider">服务提供者接口。</param>
        public AdminMenuProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 区域名称。
        /// </summary>
        public const string AreaName = "Core";

        /// <summary>
        /// 初始化菜单实例。
        /// </summary>
        /// <param name="root">根目录菜单。</param>
        public override void Init(MenuItem root)
        {
            root.AddMenu("sys", menu =>
            {
                menu.Texted("系统管理", "fa-cogs")
                    .AddMenu("tasks",
                        it => it.Texted("后台服务").Page("/Admin/Tasks/Index", area: AreaName)
                            .Allow(Permissions.Administrator));
                menu.AddMenu("notifier",
                    it => it.Texted("通知管理").Page("/Admin/Notifications/Index", area: AreaName)
                        .Allow(Permissions.Administrator));
                menu.AddMenu("sensitive",
                    it => it.Texted("敏感词汇管理").Page("/Admin/SensitiveWords/Index", area: AreaName)
                        .Allow(Permissions.Administrator));
                if (_serviceProvider.GetService<ISettingDictionaryManager>() != null)
                    menu.AddMenu("dicsettings",
                        it => it.Texted("字典管理").Page("/Admin/DictionarySettings/Index", area: AreaName)
                            .Allow(Permissions.Administrator));
            });
        }
    }
}
