using System.Collections.Generic;
using Gentings.Extensions.AspNetCore.EventLogging;
using Gentings.Identity.Permissions;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Admin.Logs
{
    /// <summary>
    /// 日志分类。
    /// </summary>
    [PermissionAuthorize(SecurityPermissions.Logs)]
    public class CategoryModel : ModelBase
    {
        private readonly IEventTypeManager _eventTypeManager;
        public CategoryModel(IEventTypeManager eventTypeManager)
        {
            _eventTypeManager = eventTypeManager;
        }

        public IEnumerable<EventType> Types { get; private set; }

        public void OnGet()
        {
            Types = _eventTypeManager.Fetch();
        }
    }
}