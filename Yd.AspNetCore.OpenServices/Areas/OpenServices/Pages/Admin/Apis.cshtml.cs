﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gentings.Extensions.OpenServices;
using Gentings.Extensions.OpenServices.ApiDocuments;
using Gentings.Security.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.OpenServices.Areas.OpenServices.Pages.Admin
{
    /// <summary>
    /// 开放服务模型。
    /// </summary>
    [PermissionAuthorize(OpenServicePermissions.Setting)]
    public class ApisModel : AdminModelBase
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
        public ApisModel(IServiceDocumentManager serviceDocumentManager, IOpenServiceManager serviceManager, IApplicationManager applicationManager)
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
            Application = await _applicationManager.FindAsync(id);
            if (Application == null)
                return NotFound();
            Services = await _applicationManager.LoadApplicationServicesAsync(id);
            Document = _serviceManager.GetGroupApiDescriptors();
            return Page();
        }

        /// <summary>
        /// 关联API。
        /// </summary>
        /// <param name="appid">应用程序Id。</param>
        /// <param name="ids">服务ID列表。</param>
        /// <returns>返回关联结果。</returns>
        public async Task<IActionResult> OnPostAddAsync(Guid appid, int[] ids)
        {
            var result = await _applicationManager.AddApplicationServicesAsync(appid, ids);
            if (result)
                return Success("你已经成功关联所选择的API到当前应用程序中！");
            return Error("关联失败，请重试！");
        }

        /// <summary>
        /// 当前应用程序包含的服务Id。
        /// </summary>
        public List<int> Services { get; private set; }
    }
}
