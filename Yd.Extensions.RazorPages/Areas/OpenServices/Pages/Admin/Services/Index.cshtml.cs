using System.Collections.Generic;
using Yd.Extensions.Controllers;
using Yd.Extensions.Controllers.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Admin.Services
{
    /// <summary>
    /// ���ŷ���ģ�͡�
    /// </summary>
    public class IndexModel : ModelBase
    {
        private readonly IServiceDocumentManager _serviceManager;
        /// <summary>
        /// ��ʼ����<see cref="IndexModel"/>��
        /// </summary>
        /// <param name="serviceManager">�����ĵ�����ӿڡ�</param>
        public IndexModel(IServiceDocumentManager serviceManager)
        {
            _serviceManager = serviceManager;
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
