using System;
using Gentings.Extensions;
using Gentings.Extensions.Events;
using Gentings.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using EventQuery = Yd.Extensions.Security.EventQuery;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.Logs
{
    /// <summary>
    /// 日志。
    /// </summary>
    [PermissionAuthorize(SecurityPermissions.Logs)]
    public class IndexModel : ModelBase
    {
        private readonly IEventManager _eventManager;
        /// <summary>
        /// 初始化类<see cref="IndexModel"/>。
        /// </summary>
        /// <param name="eventManager">事件管理接口。</param>
        public IndexModel(IEventManager eventManager)
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
        /// 查询实例。
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public EventQuery Query { get; set; }

        /// <summary>
        /// 事件列表。
        /// </summary>
        public IPageEnumerable<Event> Items { get; set; }

        /// <summary>
        /// 分页获取事件列表。
        /// </summary>
        public void OnGet()
        {
            if (Query.End != null)
                Query.End = Query.End.Value.Add(new TimeSpan(23, 59, 59));
            Query.IsChildren = true;
            Items = _eventManager.Load(Query);
        }
    }
}