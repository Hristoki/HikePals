namespace HikePals.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    using HikePals.Services.Data;
    using HikePals.Services.Data.Contracts;
    using HikePals.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;
        private readonly ICitiesService citiesService;
        private readonly ICategoriesService categoriesService;

        public TripsController(ITripsService tripsService, ICitiesService citiesService, ICategoriesService categoriesService)
        {
            this.tripsService = tripsService;
            this.citiesService = citiesService;
            this.categoriesService = categoriesService;
        }

        // GET: Administration/Trips
        public IActionResult Index()
        {
            var model = this.tripsService.GetAllTripsWithDeleted();

            return this.View(model);
        }

        // GET: Administration/Trips/Details/5
        public IActionResult Details(int id)
        {

            var trip = this.tripsService.GetByIdWithDeleted<TripViewModel>(id);
            if (trip == null)
            {
                return this.NotFound();
            }
            return this.View(trip);
        }

        // GET: Administration/Trips/Create
        public IActionResult Create()
        {
            return this.RedirectToAction("Create", nameof(HikePals.Web.Controllers.TripsController));
        }

        public IActionResult Edit(int id)
        {
            var model = this.tripsService.GetByIdWithDeleted<EditTripViewModel>(id);

            if (model == null)
            {
                return this.NotFound();
            }

            model.CityItems = this.citiesService.GetAllCities();
            model.CategoriesItems = this.categoriesService.GetAllCategoriesListItems();

            return this.View(model);
        }

        // POST: Administration/Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTripViewModel trip)
        {
            if (id != trip.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.tripsService.UpdateAsync(trip);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.tripsService.Exists(trip.Id))
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

            trip.CityItems = this.citiesService.GetAllCities();
            trip.CategoriesItems = this.categoriesService.GetAllCategoriesListItems();

            return this.View(trip);
        }

        // GET: Administration/Trips/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.tripsService.Exists(id))
            {
                return this.NotFound();
            }

            await this.tripsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Restore(int id)
        {
            await this.tripsService.RestoreTripAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
