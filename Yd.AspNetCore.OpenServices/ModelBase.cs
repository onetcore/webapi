using Yd.AspNetCore.OpenServices.Properties;

namespace Yd.AspNetCore.OpenServices
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