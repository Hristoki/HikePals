﻿namespace HikePals.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HikePals.Data;
    using HikePals.Data.Models;
    using HikePals.Services.Data;
    using HikePals.Web.ViewModels.Events;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class EventsController : AdministrationController
    {

        private readonly ITransportService transportService;
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;

        public EventsController(IEventsService eventsService, ITransportService transportService, UserManager<ApplicationUser> userManager)
        {
            this.eventsService = eventsService;
            this.transportService = transportService;
            this.userManager = userManager;
        }

        // GET: Administration/Events
        public IActionResult Index()
        {
            var model = this.eventsService.GetAllWithDeleted();
            return this.View(model);
        }

        // GET: Administration/Events/Details/5
        public IActionResult Details(int id)
        {
            var @event = this.eventsService.GetByIdWithDeleted<EventViewModel>(id);

            if (@event == null)
            {
                return this.NotFound();
            }

            return this.View(@event);
        }

        // GET: Administration/Events/Create
        //public IActionResult Create()
        //{
        //    ViewData["CreatedById"] = new SelectList(this.EventsService.Users, "Id", "Id");
        //    ViewData["TransportId"] = new SelectList(this.EventsService.Transports, "Id", "Id");
        //    ViewData["TripId"] = new SelectList(this.EventsService.Trips, "Id", "Id");
        //    return View();
        //}

        // POST: Administration/Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Title,Details,CreatedById,StartTime,EndTime,MaxGroupSize,TransportId,TripId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Event @event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        this.EventsService.Add(@event);
        //        await this.EventsService.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CreatedById"] = new SelectList(this.EventsService.Users, "Id", "Id", @event.CreatedById);
        //    ViewData["TransportId"] = new SelectList(this.EventsService.Transports, "Id", "Id", @event.TransportId);
        //    ViewData["TripId"] = new SelectList(this.EventsService.Trips, "Id", "Id", @event.TripId);
        //    return View(@event);
        //}


        // GET: Administration/Events/Edit/5

        public IActionResult Edit(int id)
        {
            var model = this.eventsService.GetById<EditEventViewModel>(id);

            if (model == null)
            {
                return this.NotFound();
            }

            var transportItems = this.transportService.GetAllTransportTypes();
            model.TransportItems = transportItems;

            return this.View(model);
        }

        // POST: Administration/Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Details,CreatedById,StartTime,EndTime,MaxGroupSize,TransportId,TripId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] EditEventViewModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                   await this.eventsService.UpdateAsync(input);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.eventsService.Exists(input.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            var transportItems = this.transportService.GetAllTransportTypes();
            input.TransportItems = transportItems;

            return this.View(input);
        }

        // POST: Administration/Events/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.eventsService.Exists(id))
            {
                return this.NotFound();
            }

            await this.eventsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Restore(int id)
        {
            //if (!this.eventsService.Exists(id))
            //{
            //    return this.NotFound();
            //}

            await this.eventsService.RestoreAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
