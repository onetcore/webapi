using Gentings.Extensions;
using Gentings.Extensions.AspNetCore.EventLogging;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Account
{
    /// <summary>
    /// 日志模型。
    /// </summary>
    public class LogModel : ModelBase
    {
        private readonly IEventManager _eventManager;
        private readonly IEventTypeManager _eventTypeManager;

        public LogModel(IEventManager eventManager, IEventTypeManager eventTypeManager)
        {
            _eventManager = eventManager;
            _eventTypeManager = eventTypeManager;
        }

        /// <summary>
        /// 获取事件类型。
        /// </summary>
        /// <param name="id">事件类型Id。</param>
        /// <returns>返回事件类型名称。</returns>
        public string GetEventType(int id) => _eventTypeManager.Find(id)?.Name;

        public IPageEnumerable<EventMessage> Model { get; set; }

        public void OnGet(EventQuery query)
        {
            query.UserId = UserId;
            Model = _eventManager.Load(query);
        }
    }
}