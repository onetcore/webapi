using System.Collections.Generic;
using System.Linq;
using Gentings.Security.Permissions;
using Microsoft.Extensions.Hosting;
using Yd.AspNetCore.Core;

namespace Yd.AspNetCore.Areas.Core.Pages.Admin.Tasks
{
    /// <summary>
    /// 后台服务列表。
    /// </summary>
    [PermissionAuthorize(CorePermissions.Task)]
    public class BackgroundServiceModel : AdminModelBase
    {
        private readonly IEnumerable<IHostedService> _hostedServices;

        /// <summary>
        /// 初始化类<see cref="BackgroundServiceModel"/>。
        /// </summary>
        /// <param name="hostedServices">后台服务接口列表。</param>
        public BackgroundServiceModel(IEnumerable<IHostedService> hostedServices)
        {
            _hostedServices = hostedServices;
        }
        /// <summary>
        /// 后台服务进程。
        /// </summary>
        public IEnumerable<Gentings.BackgroundService> HostedServices { get; private set; }

        /// <summary>
        /// 获取后台服务。
        /// </summary>
        public void OnGet()
        {
            HostedServices = _hostedServices.Select(x => x as Gentings.BackgroundService)
                .Where(x => x != null)
                .ToArray();
        }
    }
}