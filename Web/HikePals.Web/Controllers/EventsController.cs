namespace HikePals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HikePals.Common;
    using HikePals.Data.Models;
    using HikePals.Services.Data;
    using HikePals.Services.Data.Contracts;
    using HikePals.Web.ViewModels.Chat;
    using HikePals.Web.ViewModels.Events;
    using HikePals.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class EventsController : BaseController
    {
        private readonly ITransportService transportService;
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IChatService chatService;

        public EventsController(ITransportService transportService, IEventsService eventsService, UserManager<ApplicationUser> userManager, IChatService chatService)
        {
            this.transportService = transportService;
            this.eventsService = eventsService;
            this.userManager = userManager;
            this.chatService = chatService;
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
            var eventId = await this.eventsService.CreateNewEvent(input, user.Id);

            this.TempData["Message"] = "You have sucessully added a hiking event!";

            return this.RedirectToAction(nameof(this.GetById), new { id = eventId });
        }

        public async Task<IActionResult> GetById(int id)
        {
            var model = this.eventsService.GetById<EventViewModel>(id);

            if (model == null)
            {
                return this.RedirectToAction("NotFoundError", "Error");
            }

            var user = await this.userManager.GetUserAsync(this.User);
            model.UserId = user.Id;

            return this.View(model);
        }

        [AllowAnonymous]
        public IActionResult All()
        {
            var model = new AllEventAsListViewModel();
            model.Events = this.eventsService.GetAll<SingleEventListViewModel>();

            return this.View(model);
        }

        public IActionResult Edit(int id)
        {
           var model = this.eventsService.GetById<EditEventViewModel>(id);

           if (model == null)
           {
                return this.RedirectToAction("NotFoundError", "Error");
           }

           model.TransportItems = this.transportService.GetAllTransportTypes();
           return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel input)
        {
            await this.eventsService.UpdateAsync(input);
            var eventId = input.Id;

            return this.RedirectToAction(nameof(this.GetById), new { id = eventId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!this.eventsService.Exists(id))
            {
                return this.RedirectToAction("NotFoundError", "Error");
            }

            await this.eventsService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> RequestJoinEvent(int id)
        {
           var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
           var eventId = id;
           var isAdmin = this.User.IsInRole("Administrator");

           try
           {
              await this.eventsService.RequestJoinEvent(eventId, userId, isAdmin);
           }
           catch (Exception ex)
           {
               this.ModelState.AddModelError(string.Empty, ex.Message);
               return this.RedirectToAction("GetbyId", new { id = eventId });
           }

           return this.RedirectToAction("GetbyId", new { id = eventId });
        }

        public async Task<IActionResult> ApproveJoinEvent(int id, string participantId)
        {
            var eventId = id;

            try
            {
                await this.eventsService.ApproveJoinRequest(participantId, eventId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.RedirectToAction("GetbyId", new { id = eventId });
            }

             // Get previous page Url address
            var referer = this.Request
                .Headers["Referer"]
                .ToString();

            return this.Redirect(referer);
        }

        public async Task<IActionResult> Leave(int id, string participantId)
        {
            try
            {
                await this.eventsService.LeaveEvent(participantId, id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.RedirectToAction("GetbyId", new { id = id });
            }

            return this.RedirectToAction("GetbyId", new { id = id });
        }

        public async Task<IActionResult> UndoJoinRequest(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await this.eventsService.UndoJoinRequest(userId, id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.RedirectToAction("GetbyId", new { id = id });
            }

            return this.RedirectToAction("GetbyId", new { id = id });
        }
    }
}
