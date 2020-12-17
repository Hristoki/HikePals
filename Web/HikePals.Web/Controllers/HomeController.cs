namespace HikePals.Web.Controllers
{
    using System.Diagnostics;
    using HikePals.Services.Data;
    using HikePals.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetHomePageDataService getHomePageDataService;

        public HomeController(IGetHomePageDataService getHomePageDataService)
        {
            this.getHomePageDataService = getHomePageDataService;
        }

        public IActionResult Index()
        {
            var model = this.getHomePageDataService.GetCounts();
            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
