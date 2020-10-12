using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Controllers.OpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Account
{
    public class IndexModel : ModelBase
    {
        private readonly IApplicationManager _applicationManager;

        public IndexModel(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        [BindProperty(SupportsGet = true)]
        public ApplicationQuery Query { get; set; }

        public IEnumerable<Application> Applications { get; private set; }

        public void OnGet()
        {
            Query.UserId = UserId;
            Applications = _applicationManager.Fetch(Query);
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
