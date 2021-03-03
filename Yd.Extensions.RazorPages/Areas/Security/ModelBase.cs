using Yd.Extensions.RazorPages.Properties;

namespace Yd.Extensions.RazorPages.Areas.Security
{
    /// <summary>
    /// 页面模型基类。
    /// </summary>
    public abstract class ModelBase : RazorPages.ModelBase
    {
        /// <summary>
        /// 事件类型。
        /// </summary>
        protected override string EventType => Resources.EventType_Users;
    }
}