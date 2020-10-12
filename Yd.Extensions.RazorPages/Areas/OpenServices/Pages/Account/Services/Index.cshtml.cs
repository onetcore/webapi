using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Controllers;
using Yd.Extensions.Controllers.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Account.Services
{
    /// <summary>
    /// 开放服务模型。
    /// </summary>
    public class IndexModel : ModelBase
    {
        /// <summary>
        /// 服务管理接口。
        /// </summary>
        public IOpenServiceManager ServiceManager { get; }
        private readonly IServiceDocumentManager _serviceManager;
        private readonly IApplicationManager _applicationManager;

        /// <summary>
        /// 初始化类<see cref="IndexModel"/>。
        /// </summary>
        /// <param name="serviceDocumentManager">服务文档管理接口。</param>
        /// <param name="serviceManager">开放服务管理接口。</param>
        /// <param name="applicationManager">应用程序管理接口。</param>
        public IndexModel(IServiceDocumentManager serviceDocumentManager, IOpenServiceManager serviceManager, IApplicationManager applicationManager)
        {
            ServiceManager = serviceManager;
            _serviceManager = serviceDocumentManager;
            _applicationManager = applicationManager;
        }

        /// <summary>
        /// 文档列表。
        /// </summary>
        public IDictionary<string, IEnumerable<ApiDescriptor>> Document { get; private set; }

        /// <summary>
        /// 当前应用程序。
        /// </summary>
        public Application Application { get; private set; }

        /// <summary>
        /// 获取文档列表。
        /// </summary>
        /// <param name="id">应用程序Id。</param>
        public async Task<IActionResult> OnGet(Guid id)
        {
            Application = await _applicationManager.FindAsync(x => x.Id == id && x.UserId == UserId);
            if (Application == null)
                return NotFound();
            Services = await _applicationManager.LoadApplicationServicesAsync(id);
            Document = _serviceManager.GetGroupApiDescriptors();
            return Page();
        }

        /// <summary>
        /// 当前应用程序包含的服务Id。
        /// </summary>
        public List<int> Services { get; private set; }
    }
}
