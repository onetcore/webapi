﻿using Gentings;
using System;

namespace Yd.Extensions
{
    /// <summary>
    /// 网站配置。
    /// </summary>
    public class SiteSettings
    {
        /// <summary>
        /// 推广链接个数。
        /// </summary>
        public const int AliasCount = 5;

        /// <summary>
        /// 网站名称。
        /// </summary>
        public string SiteName { get; set; } = "云顶创联";

        /// <summary>
        /// 简称。
        /// </summary>
        public string ShortName { get; set; } = "云顶创联";

        /// <summary>
        /// Logo地址。
        /// </summary>
        public string LogoUrl { get; set; } = "/images/logo.svg";

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; } = "至力于APP，小程序一体化专业代工厂";

        /// <summary>
        /// 后台管理导航栏是否在上面。
        /// </summary>
        public bool IsTopMenu { get; set; }

        private string _copyright;
        /// <summary>
        /// 版权信息。
        /// </summary>
        public string Copyright
        {
            get => _copyright ??= "$year &copy; www.xmydcl.com ver $version";
            set => _copyright = value;
        }

        /// <summary>
        /// 头部代码。
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 尾部代码。
        /// </summary>
        public string Footer { get; set; }

        /// <summary>
        /// 替换后的版本信息。
        /// </summary>
        public string ReplacedCopyright => Copyright?.Replace("$version", Cores.Version.ToString(3))
            .Replace("$year", DateTime.Now.Year.ToString());
    }
}
