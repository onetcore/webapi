using Yd.Extensions.RazorPages.Properties;

namespace Yd.Extensions.RazorPages.Areas.Emails
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    public abstract class ModelBase : RazorPages.ModelBase
    {
        /// <summary>
        /// 事件类型。
        /// </summary>
        protected override string EventType => Resources.EventType_Emails;
    }
}