using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.ApiOpenServices;
using Yd.Extensions.ApiOpenServices.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Admin.Services
{
    /// <summary>
    /// �������顣
    /// </summary>
    public class ResultModel : ModelBase
    {
        private readonly IServiceDocumentManager _documentManager;
        /// <summary>
        /// ��ʼ����<see cref="ResultModel"/>��
        /// </summary>
        /// <param name="documentManager">�ĵ�����ӿڡ�</param>
        public ResultModel(IServiceDocumentManager documentManager)
        {
            _documentManager = documentManager;
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        public ApiDescriptor ApiDescriptor { get; private set; }

        /// <summary>
        /// ��ȡTokenҳ�档
        /// </summary>
        public IActionResult OnGet(string method, string route)
        {
            ApiDescriptor = _documentManager.GetApiDescriptors()
                .SingleOrDefault(x => x.HttpMethod == method && x.RouteTemplate == route);
            if (ApiDescriptor == null)
                return NotFound();
            return Page();
        }
    }
}
