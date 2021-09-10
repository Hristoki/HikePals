namespace HikePals.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using HikePals.Common;
    using HikePals.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : BaseController
    {
        [Route("/Error/500")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult InternalServerError()
        {
            var errorViewModel = new ErrorViewModel
            {
                StatusCode = GlobalConstants.InternalServerError,
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
            };

            return this.View(errorViewModel);
        }

        [Route("/Error/404")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult NotFoundError()
        {
            var errorViewModel = new ErrorViewModel();
            errorViewModel.StatusCode = GlobalConstants.NotFound;

            if (this.TempData["ErrorParams"] is Dictionary<string, string> dict)
            {
                errorViewModel.RequestId = dict["RequestId"];
                errorViewModel.RequestPath = dict["RequestPath"];
            }

            if (errorViewModel.RequestId == null)
            {
                errorViewModel.RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
            }

            return this.View(errorViewModel);
        }
    }
}
