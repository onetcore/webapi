using System.Collections.Generic;
using Gentings.Extensions.Events;
using Gentings.Security.Permissions;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.Logs
{
    /// <summary>
    /// 日志分类。
    /// </summary>
    [PermissionAuthorize(SecurityPermissions.Logs)]
    public class CategoryModel : ModelBase
    {
        private readonly IEventManager _eventManager;

        public CategoryModel(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public IEnumerable<EventType> Types { get; private set; }

        public void OnGet()
        {
            Types = _eventManager.GetEventTypes();
        }
    }
}