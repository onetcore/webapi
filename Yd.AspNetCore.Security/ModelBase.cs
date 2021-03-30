using Yd.AspNetCore.Security.Properties;

namespace Yd.AspNetCore.Security
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