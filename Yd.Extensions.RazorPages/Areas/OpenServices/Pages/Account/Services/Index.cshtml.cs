using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Controllers;
using Yd.Extensions.Controllers.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Account.Services
{
    /// <summary>
    /// ���ŷ���ģ�͡�
    /// </summary>
    public class IndexModel : ModelBase
    {
        /// <summary>
        /// �������ӿڡ�
        /// </summary>
        public IOpenServiceManager ServiceManager { get; }
        private readonly IServiceDocumentManager _serviceManager;
        private readonly IApplicationManager _applicationManager;

        /// <summary>
        /// ��ʼ����<see cref="IndexModel"/>��
        /// </summary>
        /// <param name="serviceDocumentManager">�����ĵ�����ӿڡ�</param>
        /// <param name="serviceManager">���ŷ������ӿڡ�</param>
        /// <param name="applicationManager">Ӧ�ó������ӿڡ�</param>
        public IndexModel(IServiceDocumentManager serviceDocumentManager, IOpenServiceManager serviceManager, IApplicationManager applicationManager)
        {
            ServiceManager = serviceManager;
            _serviceManager = serviceDocumentManager;
            _applicationManager = applicationManager;
        }

        /// <summary>
        /// �ĵ��б�
        /// </summary>
        public IDictionary<string, IEnumerable<ApiDescriptor>> Document { get; private set; }

        /// <summary>
        /// ��ǰӦ�ó���
        /// </summary>
        public Application Application { get; private set; }

        /// <summary>
        /// ��ȡ�ĵ��б�
        /// </summary>
        /// <param name="id">Ӧ�ó���Id��</param>
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
        /// ��ǰӦ�ó�������ķ���Id��
        /// </summary>
        public List<int> Services { get; private set; }
    }
}
