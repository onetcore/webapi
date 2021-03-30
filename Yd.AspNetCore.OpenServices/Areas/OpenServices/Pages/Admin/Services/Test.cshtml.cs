using System;
using System.Linq;
using Gentings.Extensions.OpenServices;
using Gentings.Extensions.OpenServices.ApiDocuments;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.OpenServices.Areas.OpenServices.Pages.Admin.Services
{
    /// <summary>
    /// ���Դ��ڡ�
    /// </summary>
    public class TestModel : AdminModelBase
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
                .SingleOrDefault(x => x.RouteTemplate.Equals(id, StringComparison.OrdinalIgnoreCase) && x.HttpMethod == method);
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
