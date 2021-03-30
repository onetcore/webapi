using Yd.AspNetCore.Emails.Properties;

namespace Yd.AspNetCore.Emails
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    public abstract class ModelBase : AspNetCore.ModelBase
    {
        /// <summary>
        /// 事件类型。
        /// </summary>
        protected override string EventType => Resources.EventType;
    }
}