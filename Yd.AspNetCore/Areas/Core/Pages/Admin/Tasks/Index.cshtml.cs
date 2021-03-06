﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gentings.Security.Permissions;
using Gentings.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yd.AspNetCore.Core;

namespace Yd.AspNetCore.Areas.Core.Pages.Admin.Tasks
{
    /// <summary>
    /// 后台服务列表。
    /// </summary>
    [PermissionAuthorize(CorePermissions.Task)]
    public class IndexModel : AdminModelBase
    {
        private readonly ITaskManager _taskManager;

        public IndexModel(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public IEnumerable<TaskDescriptor> Tasks { get; private set; }

        public async Task OnGetAsync()
        {
            Tasks = await _taskManager.LoadTasksAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var tasks = await _taskManager.LoadTasksAsync();
            return Success(
                tasks.Select(x => new
                {
                    x.Id,
                    LastExecuted = x.LastExecuted?.ToString("yyyy-MM-dd HH:mm:ss"),
                    NextExecuting = x.NextExecuting < DateTime.Now ? null : x.NextExecuting.ToString("yyyy-MM-dd HH:mm:ss")
                }));
        }
    }
}