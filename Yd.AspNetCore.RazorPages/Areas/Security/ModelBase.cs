using Gentings.Extensions.Settings;
using Yd.AspNetCore.RazorPages.Properties;
using Yd.Extensions;

namespace Yd.AspNetCore.RazorPages.Areas.Security
{
    /// <summary>
    /// 页面模型基类。
    /// </summary>
    public abstract class ModelBase : RazorPages.ModelBase
    {
        private SecuritySettings _settings;
        /// <summary>
        /// 安全配置。
        /// </summary>
        public SecuritySettings Settings => _settings ??= GetRequiredService<ISettingsManager>()
            .GetSettings<SecuritySettings>();

        /// <summary>
        /// 事件类型。
        /// </summary>
        protected override string EventType => Resources.EventType_Users;

    }
}