using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yd.Extensions.Controllers;
using Yd.Extensions.Controllers.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Admin.Services
{
    /// <summary>
    /// ���Դ��ڡ�
    /// </summary>
    public class TestModel : PageModel
    {
        private readonly IServiceDocumentManager _serviceManager;
        /// <summary>
        /// ��ʼ����<see cref="TestModel"/>��
        /// </summary>
        /// <param name="serviceManager">�����ĵ�����ӿڡ�</param>
        public TestModel(IServiceDocumentManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// ��ȡAPIʵ����
        /// </summary>
        /// <param name="id">API·�ɡ�</param>
        /// <param name="method">������</param>
        public IActionResult OnGet(string id, string method = "GET")
        {
            Api = _serviceManager.GetApiDescriptors()
                .SingleOrDefault(x => x.RouteTemplate.Equals(id, StringComparison.OrdinalIgnoreCase) && x.HttpMethod.Method == method);
            if (Api == null)
                return NotFound();
            return Page();
        }

        /// <summary>
        /// ��ǰAPIʵ����
        /// </summary>
        public ApiDescriptor Api { get; private set; }
    }
}
