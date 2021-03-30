using System.Collections.Generic;
using Gentings.Extensions.OpenServices;
using Gentings.Extensions.OpenServices.ApiDocuments;

namespace Yd.AspNetCore.OpenServices.Areas.OpenServices.Pages.Admin.Services
{
    /// <summary>
    /// ���ŷ���ģ�͡�
    /// </summary>
    public class IndexModel : AdminModelBase
    {
        /// <summary>
        /// �������ӿڡ�
        /// </summary>
        public IOpenServiceManager ServiceManager { get; }
        private readonly IServiceDocumentManager _serviceManager;

        /// <summary>
        /// ��ʼ����<see cref="IndexModel"/>��
        /// </summary>
        /// <param name="serviceDocumentManager">�����ĵ�����ӿڡ�</param>
        /// <param name="serviceManager">���ŷ������ӿڡ�</param>
        public IndexModel(IServiceDocumentManager serviceDocumentManager, IOpenServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
            _serviceManager = serviceDocumentManager;
        }
        /// <summary>
        /// �ĵ��б�
        /// </summary>
        public IDictionary<string, IEnumerable<ApiDescriptor>> Document { get; private set; }
        /// <summary>
        /// ��ȡ�ĵ��б�
        /// </summary>
        public void OnGet()
        {
            Document = _serviceManager.GetGroupApiDescriptors();
        }
    }
}
