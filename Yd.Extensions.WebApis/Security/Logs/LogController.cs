using System.Threading.Tasks;
using Gentings.Extensions.Events;
using Microsoft.AspNetCore.Mvc;
using EventQuery = Yd.Extensions.Security.EventQuery;

namespace Yd.Extensions.WebApis.Security.Logs
{
    /// <summary>
    /// 用户日志。
    /// </summary>
    public class LogController : AccountControllerBase
    {
        private readonly IEventManager _eventManager;

        /// <summary>
        /// 初始化类<see cref="LogController"/>。
        /// </summary>
        /// <param name="eventManager">用户日志管理实例。</param>
        public LogController(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        /// <summary>
        /// 获取日志列表。
        /// </summary>
        /// <param name="query">日志查询实例。</param>
        /// <returns>返回日志列表结果。</returns>
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] EventQuery query)
        {
            query.UserId = UserId;
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
            var types = await _eventManager.GetEventTypesAsync();
            return OkResult(types);
        }
    }
}