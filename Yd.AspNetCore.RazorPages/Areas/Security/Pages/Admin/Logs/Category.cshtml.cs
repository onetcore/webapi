using System.Collections.Generic;
using Gentings.Extensions.AspNetCore.EventLogging;
using Gentings.Identity.Permissions;

namespace Yd.AspNetCore.RazorPages.Areas.Security.Pages.Admin.Logs
{
    /// <summary>
    /// 日志分类。
    /// </summary>
    [PermissionAuthorize(Yd.Extensions.Permissions.Administrator)]
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