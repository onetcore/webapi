using System.Collections.Generic;
using Yd.Extensions.Controllers;
using Yd.Extensions.Controllers.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Admin.Services
{
    /// <summary>
    /// 开放服务模型。
    /// </summary>
    public class IndexModel : ModelBase
    {
        private readonly IServiceDocumentManager _serviceManager;
        /// <summary>
        /// 初始化类<see cref="IndexModel"/>。
        /// </summary>
        /// <param name="serviceManager">服务文档管理接口。</param>
        public IndexModel(IServiceDocumentManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        /// <summary>
        /// 文档列表。
        /// </summary>
        public IDictionary<string, IEnumerable<ApiDescriptor>> Document { get; private set; }
        /// <summary>
        /// 获取文档列表。
        /// </summary>
        public void OnGet()
        {
            Document = _serviceManager.GetGroupApiDescriptors();
        }
    }
}
