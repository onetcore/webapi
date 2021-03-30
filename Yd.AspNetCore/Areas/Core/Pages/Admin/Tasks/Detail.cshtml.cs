using Gentings.Security.Permissions;
using Gentings.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yd.AspNetCore.Core;

namespace Yd.AspNetCore.Areas.Core.Pages.Admin.Tasks
{
    /// <summary>
    /// 后台服务详情。
    /// </summary>
    [PermissionAuthorize(CorePermissions.Task)]
    public class DetailModel : AdminModelBase
    {
        private readonly ITaskManager _taskManager;

        public DetailModel(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public TaskDescriptor Descriptor { get; private set; }

        public IActionResult OnGet(int id)
        {
            Descriptor = _taskManager.GeTask(id);
            if (Descriptor == null)
                return NotFound();
            return Page();
        }
    }
}