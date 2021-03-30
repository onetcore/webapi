using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gentings.Extensions.OpenServices;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.OpenServices;

namespace Yd.AspNetCore.OpenServices.Areas.OpenServices.Pages.Account
{
    public class IndexModel : AccountModelBase
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
