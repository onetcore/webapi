using System.Threading.Tasks;
using Gentings.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.Security.Controllers.Admin
{
    /// <summary>
    /// 后台服务控制器。
    /// </summary>
    public class TaskController : AdminControllerBase
    {
        private readonly ITaskManager _taskManager;
        /// <summary>
        /// 初始化类<see cref="TaskController"/>。
        /// </summary>
        /// <param name="taskManager">后台服务管理实例。</param>
        public TaskController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        /// <summary>
        /// 获取后台服务列表。
        /// </summary>
        /// <returns>返回服务列表。</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskManager.LoadTasksAsync();
            return OkResult(tasks);
        }
    }
}