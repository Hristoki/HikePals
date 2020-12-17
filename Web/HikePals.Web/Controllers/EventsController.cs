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

            return this.RedirectToAction(nameof(this.GetById));
        }

        public IActionResult GetById(int id)
        {
            var model = this.eventsService.GetById<EventViewModel>(id);
            return this.View(model);
        }

        public IActionResult All()
        {
            var model = new AllEventAsListViewModel();
            model.Events = this.eventsService.GetAllEvents();

            return this.View(model);
        }

        public IActionResult Edit(int id)
        {
           var model = this.eventsService.GetById<EditEventViewModel>(id);
           model.TransportItems = this.transportService.GetAllTransportTypes();
           return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel input)
        {

            //TO DO: Create BaseModel that contains Event Id and pass it to Redirection
            await this.eventsService.UpdateAsync(input);
            var eventId = input.Id;
            return this.RedirectToAction("GetbyId", new { id = eventId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.eventsService.DeleteAsync(id);
            var model = this.eventsService.GetAllEvents();
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
