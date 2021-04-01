using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gentings.Extensions.OpenServices;
using Gentings.Extensions.OpenServices.ApiDocuments;
using Gentings.Security.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.OpenServices.Areas.OpenServices.Pages.Admin
{
    /// <summary>
    /// ���ŷ���ģ�͡�
    /// </summary>
    [PermissionAuthorize(OpenServicePermissions.Setting)]
    public class ApisModel : AdminModelBase
    {
        /// <summary>
        /// ��������ӿڡ�
        /// </summary>
        public IOpenServiceManager ServiceManager { get; }
        private readonly IServiceDocumentManager _serviceManager;
        private readonly IApplicationManager _applicationManager;

        /// <summary>
        /// ��ʼ����<see cref="IndexModel"/>��
        /// </summary>
        /// <param name="serviceDocumentManager">�����ĵ������ӿڡ�</param>
        /// <param name="serviceManager">���ŷ�������ӿڡ�</param>
        /// <param name="applicationManager">Ӧ�ó�������ӿڡ�</param>
        public ApisModel(IServiceDocumentManager serviceDocumentManager, IOpenServiceManager serviceManager, IApplicationManager applicationManager)
        {
            ServiceManager = serviceManager;
            _serviceManager = serviceDocumentManager;
            _applicationManager = applicationManager;
        }

        /// <summary>
        /// �ĵ��б���
        /// </summary>
        public IDictionary<string, IEnumerable<ApiDescriptor>> Document { get; private set; }

        /// <summary>
        /// ��ǰӦ�ó���
        /// </summary>
        public Application Application { get; private set; }

        /// <summary>
        /// ��ȡ�ĵ��б���
        /// </summary>
        /// <param name="id">Ӧ�ó���Id��</param>
        public async Task<IActionResult> OnGet(Guid id)
        {
            Application = await _applicationManager.FindAsync(id);
            if (Application == null)
                return NotFound();
            Services = await _applicationManager.LoadApplicationServicesAsync(id);
            Document = _serviceManager.GetGroupApiDescriptors();
            return Page();
        }

        /// <summary>
        /// ����API��
        /// </summary>
        /// <param name="appid">Ӧ�ó���Id��</param>
        /// <param name="ids">����ID�б���</param>
        /// <returns>���ع��������</returns>
        public async Task<IActionResult> OnPostAddAsync(Guid appid, int[] ids)
        {
            var result = await _applicationManager.AddApplicationServicesAsync(appid, ids);
            if (result)
                return Success("���Ѿ��ɹ�������ѡ���API����ǰӦ�ó����У�");
            return Error("����ʧ�ܣ������ԣ�");
        }

        /// <summary>
        /// ��ǰӦ�ó�������ķ���Id��
        /// </summary>
        public List<int> Services { get; private set; }
    }
}