namespace HikePals.Web.Areas.Administration.Controllers
{
    using HikePals.Data.Models;
    using HikePals.Services.Data;
    using HikePals.Services.Data.UserServices;
    using HikePals.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ITripsService tripsService;
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public DashboardController(ITripsService tripsService, IEventsService eventsService, UserManager<ApplicationUser> userManager, IUsersService usersService)
        {
            this.tripsService = tripsService;
            this.eventsService = eventsService;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var model = new DashboardIndexViewModel();
            model.TripsCount = this.tripsService.GetAllTripsCount();
            model.EventsCount = this.eventsService.GetAllEventsCount();
            model.UsersCount = this.usersService.GetAllUsersCount();

            return this.View(model);
        }
    }
}
