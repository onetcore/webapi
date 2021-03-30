using Yd.Extensions.RazorPages.Properties;

namespace Yd.AspNetCore.Storages
{
    /// <summary>
    /// 页面模型基类。
    /// </summary>
    public abstract class ModelBase : AspNetCore.ModelBase
    {
        /// <summary>
        /// 事件类型。
        /// </summary>
        protected override string EventType => Resources.EventType;
    }
}