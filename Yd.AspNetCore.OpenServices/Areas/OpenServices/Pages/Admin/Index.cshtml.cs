using System;
using System.Threading.Tasks;
using Gentings.Extensions;
using Gentings.Extensions.OpenServices;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.OpenServices.Areas.OpenServices.Pages.Admin
{
    public class IndexModel : AdminModelBase
    {
        private readonly IApplicationManager _applicationManager;

        public IndexModel(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        [BindProperty(SupportsGet = true)]
        public ApplicationQuery Query { get; set; }

        public IPageEnumerable<Application> Applications { get; private set; }

        public void OnGet()
        {
            Applications = _applicationManager.Load(Query);
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid[] ids)
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择应用后再进行删除操作！");
            var result = await _applicationManager.DeleteAsync(ids);
            return Json(result, "应用");
        }
    }
}
