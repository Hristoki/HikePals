namespace HikePals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HikePals.Data.Models;
    using HikePals.Services.Data;
    using HikePals.Services.Data.Contracts;
    using HikePals.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TripsController : Controller
    {
        private readonly ICitiesService citiesService;
        private readonly ICategoriesService categoriesService;
        private readonly ITripsService tripsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public TripsController(ICitiesService citiesService, ICountriesService countryService, ICategoriesService categoriesService, ITransportService transportService, ITripsService tripsService, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment, IRatingsService ratingsService)
        {
            this.citiesService = citiesService;
            this.categoriesService = categoriesService;
            this.tripsService = tripsService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult CreateTrip()
        {
            var viewModel = new CreateTripInputViewModel();

            viewModel.CityItems = this.citiesService.GetAllCities();
            viewModel.CategoryItems = this.categoriesService.GetAllCategoriesAsListItems();

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripInputViewModel input)
        {

            if (!this.ModelState.IsValid)
            {
                input.CategoryItems = this.categoriesService.GetAllCategoriesAsListItems();
                input.CityItems = this.citiesService.GetAllCities();
                return this.View(input);
            }

            var imagesDirPath = $"{this.environment.WebRootPath}\\images";

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.tripsService.AddNewTrip(input, user.Id, imagesDirPath);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoryItems = this.categoriesService.GetAllCategoriesAsListItems();
                input.CityItems = this.citiesService.GetAllCities();
                return this.View(input);
            }

            this.TempData["Message"] = "You have sucessully added a hiking route!";

            return this.RedirectToAction("All");
        }

        public IActionResult All([FromQuery] AllTripsViewModel query)
        {
            var model = this.tripsService.GetAllTrips(query.SearchTerm, query.Category, query.Sorting, query.CurrentPage, AllTripsViewModel.TripPerPage);

            var categories = this.categoriesService.All();
            model.Categories = categories;

            return this.View(model);
        }

        public IActionResult ByCategory(int id)
        {
            var model = this.tripsService.GetAllTripsByCategory<TripViewModel>(id);
            return this.View("All", model);
        }

        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var model = this.tripsService.GetById<TripViewModel>(id);

            if (this.ModelBinderFactory == null)
            {
                return this.RedirectToAction("NotFoundError", "Error");
            }

            var user = await this.userManager.GetUserAsync(this.User);
            model.UserId = user.Id;

            return this.View(model);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var model = this.tripsService.GetById<EditTripViewModel>(id);

            model.CityItems = this.citiesService.GetAllCities();
            model.CategoriesItems = this.categoriesService.GetAllCategoriesAsListItems();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTripViewModel input)
        {

            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllCategoriesAsListItems();
                input.CityItems = this.citiesService.GetAllCities();

                this.View(input);
            }

            await this.tripsService.UpdateAsync(input);

            var model = this.tripsService.GetById<TripViewModel>(input.Id);

            if (model == null)
            {
                return this.RedirectToAction("NotFoundError", "Error");
            }

            return this.RedirectToAction(nameof(this.GetById), new { id = input.Id });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.tripsService.Exists(id))
            {
               return this.RedirectToAction("NotFoundError", "Error");
            }

            await this.tripsService.DeleteAsync(id);
            return this.RedirectToAction("All");
        }
    }
}
