using Yd.Extensions.Properties;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    public abstract class ControllerBase : Extensions.ControllerBase
    {
        /// <summary>
        /// 事件类型。
        /// </summary>
        protected override string EventType => Resources.EventType_Users;
    }
}