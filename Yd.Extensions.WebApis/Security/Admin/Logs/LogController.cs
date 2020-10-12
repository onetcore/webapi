using System.Threading.Tasks;
using Gentings.Extensions.EventLogging;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.Extensions.WebApis.Security.Admin.Logs
{
    /// <summary>
    /// 用户日志。
    /// </summary>
    public class LogController : AdminControllerBase
    {
        private readonly IEventManager _eventManager;
        private readonly IEventTypeManager _eventTypeManager;

        /// <summary>
        /// 初始化类<see cref="LogController"/>。
        /// </summary>
        /// <param name="eventManager">用户日志管理实例。</param>
        /// <param name="eventTypeManager">事件类型管理实例。</param>
        public LogController(IEventManager eventManager, IEventTypeManager eventTypeManager)
        {
            _eventManager = eventManager;
            _eventTypeManager = eventTypeManager;
        }

        /// <summary>
        /// 获取日志列表。
        /// </summary>
        /// <param name="query">日志查询实例。</param>
        /// <returns>返回日志列表结果。</returns>
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] EventQuery query)
        {
            query.RoleLevel = Role.RoleLevel;
            var events = await _eventManager.LoadAsync(query);
            return OkResult(events);
        }

        /// <summary>
        /// 获取日志类型列表。
        /// </summary>
        /// <returns>返回日志类型列表结果。</returns>
        [HttpGet("types")]
        public async Task<IActionResult> LoadEventTypes()
        {
            var types = await _eventTypeManager.FetchAsync();
            return OkResult(types);
        }

        /// <summary>
        /// 保存日志类型。
        /// </summary>
        /// <param name="eventType">日志类型实例。</param>
        /// <returns>返回保存结果。</returns>
        [HttpPost("save-type")]
        public async Task<IActionResult> SaveEventType(EventType eventType)
        {
            var result = await _eventTypeManager.SaveAsync(eventType);
            if (result)
                return OkResult();
            return BadResult(result.ToString("日志类型"));
        }
    }
}