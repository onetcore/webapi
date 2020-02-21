using Gentings;
using System;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("title")]
        public string SiteName { get; set; } = "云顶创联";

        /// <summary>
        /// 简称。
        /// </summary>
        public string ShortName { get; set; } = "云顶创联";

        /// <summary>
        /// Logo地址。
        /// </summary>
        public string LogoUrl { get; set; } = "/images/logo.png";

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; }= "至力于APP，小程序一体化专业代工厂";

        private string _copyright;
        /// <summary>
        /// 版权信息。
        /// </summary>
        public string Copyright
        {
            get => _copyright ??= "$year www.xmydcl.com ver $version".Replace("$version", Cores.Version.ToString(3)).Replace("$year", DateTime.Now.Year.ToString());
            set => _copyright = value;
        }

        /// <summary>
        /// 导航模板：light|dark。
        /// </summary>
        public string NavTheme { get; set; } = "dark";

        /// <summary>
        /// 主题颜色。
        /// </summary>
        public string PrimaryColor { get; set; } = "daybreak";

        /// <summary>
        /// 布局：sidemenu | topmenu。
        /// </summary>
        public string Layout { get; set; } = "sidemenu";

        /// <summary>
        /// 内容布局：Fluid | Fixed。
        /// </summary>
        public string ContentWidth { get; set; } = "Fluid";

        /// <summary>
        /// 固定标题头。
        /// </summary>
        public bool FixedHeader { get; set; }

        /// <summary>
        /// 自动隐藏标题头。
        /// </summary>
        public bool AutoHideHeader { get; set; }

        /// <summary>
        /// 固定侧边栏。
        /// </summary>
        public bool FixSiderbar { get; set; }

        /// <summary>
        /// 菜单配置。
        /// </summary>
        public class MenuSetting
        {
            /// <summary>
            /// 是否本地化。
            /// </summary>
            public bool Locale { get; set; } = true;

            /// <summary>
            /// 默认全部打开。
            /// </summary>
            public bool DefaultOpenAll { get; set; } = false;
        }

        /// <summary>
        /// 菜单配置。
        /// </summary>
        public MenuSetting Menu { get; set; }

        /// <summary>
        /// 是否启用浏览器离线缓存。
        /// </summary>
        public bool Pwa { get; set; }

        /// <summary>
        /// 自定义图标CDN地址。
        /// </summary>
        public string IconfontUrl { get; set; }

        /// <summary>
        /// 弱色显示。
        /// </summary>
        public bool ColorWeak { get; set; }

        /// <summary>
        /// 是否需要确认电子邮件。
        /// </summary>
        public bool RequiredEmailConfirmed { get; set; }

        /// <summary>
        /// 开放注册。
        /// </summary>
        public bool Registrable { get; set; }

        /// <summary>
        /// 登录后的默认转向。
        /// </summary>
        public LoginDirection LoginDirection { get; set; } = LoginDirection.Default;

        /// <summary>
        /// 是否开启二次验证。
        /// </summary>
        public bool RequiredTwoFactorEnabled { get; set; }
    }
}
