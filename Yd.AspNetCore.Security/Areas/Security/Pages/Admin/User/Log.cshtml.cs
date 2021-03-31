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
        /// <summary>
        /// 初始化类<see cref="LogModel"/>。
        /// </summary>
        /// <param name="eventManager">事件管理接口。</param>
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

        /// <summary>
        /// 事件列表。
        /// </summary>
        public IPageEnumerable<Event> Items { get; set; }

        /// <summary>
        /// 分页获取事件列表。
        /// </summary>
        /// <param name="query">事件查询实例。</param>
        public void OnGet(EventQuery query)
        {
            query.UserId = UserId;
            Items = _eventManager.Load(query);
        }
    }
}