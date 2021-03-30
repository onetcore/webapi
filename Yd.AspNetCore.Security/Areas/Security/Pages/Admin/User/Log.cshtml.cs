using Gentings.Extensions;
using Gentings.Extensions.Events;
using EventQuery = Yd.Extensions.Security.EventQuery;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.User
{
    /// <summary>
    /// 日志模型。
    /// </summary>
    public class LogModel : ModelBase
    {
        private readonly IEventManager _eventManager;

        public LogModel(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        /// <summary>
        /// 获取事件类型。
        /// </summary>
        /// <param name="id">事件类型Id。</param>
        /// <returns>返回事件类型名称。</returns>
        public string GetEventType(int id) => _eventManager.GetEventType(id)?.Name;

        public IPageEnumerable<Event> Model { get; set; }

        public void OnGet(EventQuery query)
        {
            query.UserId = UserId;
            Model = _eventManager.Load(query);
        }
    }
}