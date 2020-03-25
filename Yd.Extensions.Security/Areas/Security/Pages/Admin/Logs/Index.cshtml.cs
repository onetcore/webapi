using Gentings.AspNetCore.EventLogging;
using Gentings.Extensions;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.Security.Areas.Security.Pages.Admin.Logs
{
    /// <summary>
    /// 日志。
    /// </summary>
    [PermissionAuthorize(Security.Permissions.Logs)]
    public class IndexModel : ModelBase
    {
        private readonly IEventManager _eventManager;
        private readonly IEventTypeManager _eventTypeManager;

        public IndexModel(IEventManager eventManager, IEventTypeManager eventTypeManager)
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

        /// <summary>
        /// 查询实例。
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public EventQuery Query { get; set; }

        public IPageEnumerable<EventMessage> Model { get; set; }

        public void OnGet()
        {
            Query.RoleLevel = Role.RoleLevel;
            Model = _eventManager.Load(Query);
        }
    }
}