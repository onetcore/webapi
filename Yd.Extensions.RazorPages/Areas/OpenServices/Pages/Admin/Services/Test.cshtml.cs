using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yd.Extensions.Controllers;
using Yd.Extensions.Controllers.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Admin.Services
{
    /// <summary>
    /// 调试窗口。
    /// </summary>
    public class TestModel : PageModel
    {
        private readonly IServiceDocumentManager _serviceManager;
        /// <summary>
        /// 初始化类<see cref="TestModel"/>。
        /// </summary>
        /// <param name="serviceManager">服务文档管理接口。</param>
        public TestModel(IServiceDocumentManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// 获取API实例。
        /// </summary>
        /// <param name="id">API路由。</param>
        /// <param name="method">方法。</param>
        public IActionResult OnGet(string id, string method = "GET")
        {
            Api = _serviceManager.GetApiDescriptors()
                .SingleOrDefault(x => x.RouteTemplate.Equals(id, StringComparison.OrdinalIgnoreCase) && x.HttpMethod.Method == method);
            if (Api == null)
                return NotFound();
            return Page();
        }

        /// <summary>
        /// 当前API实例。
        /// </summary>
        public ApiDescriptor Api { get; private set; }
    }
}
