namespace HikePals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HikePals.Data.Models;
    using HikePals.Services.Data;
    using HikePals.Web.ViewModels.Events;
    using HikePals.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class EventsController : BaseController
    {
        private readonly ITransportService transportService;
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;

        public EventsController(ITransportService transportService, IEventsService eventsService, UserManager<ApplicationUser> userManager)
        {
            this.transportService = transportService;
            this.eventsService = eventsService;
            this.userManager = userManager;
        }


        public IActionResult Create(int id)
        {

            var model = this.eventsService.MapTripData(id);
            model.TransportItems = this.transportService.GetAllTransportTypes();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventInputViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.eventsService.CreateNewEvent(input, user.Id);

            return this.RedirectToAction("GetById");
        }

        public IActionResult GetById(CreateEventInputViewModel input)
        {
            var model = this.eventsService.GetById<EventViewModel>(input.TripId);
            return this.View("GetById");
        }

    }
}
