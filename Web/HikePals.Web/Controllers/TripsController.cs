namespace HikePals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HikePals.Data.Models;
    using HikePals.Services.Data;
    using HikePals.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TripsController : Controller
    {
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countryService;
        private readonly ILocationCategoriesService categoriesService;
        private readonly ITransportService transportService;
        private readonly ITripsService tripsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public TripsController(ICitiesService citiesService, ICountriesService countryService, ILocationCategoriesService categoriesService, ITransportService transportService, ITripsService tripsService, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment, IRatingsService ratingsService)
        {
            this.citiesService = citiesService;
            this.countryService = countryService;
            this.categoriesService = categoriesService;
            this.transportService = transportService;
            this.tripsService = tripsService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult CreateTrip()
        {
           var viewModel = new CreateTripInputViewModel();

           viewModel.CityItems = this.citiesService.GetAllCities();
           viewModel.CategoryItems = this.categoriesService.GetAllLocationCategories();

           return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripInputViewModel input)
        {

            if (!this.ModelState.IsValid)
            {
                input.CategoryItems = this.categoriesService.GetAllLocationCategories();
                input.CityItems = this.citiesService.GetAllCities();
                return this.View(input);
            }

            //TODO: Add to constants

            var imagesDirPath = $"{this.environment.WebRootPath}\\images";

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
            await this.tripsService.AddNewTrip(input, user.Id, imagesDirPath);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoryItems = this.categoriesService.GetAllLocationCategories();
                input.CityItems = this.citiesService.GetAllCities();
                return this.View(input);
            }

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {

            var model = this.tripsService.GetAllTrips();
            return this.View(model);
        }

        public IActionResult GetById(int id)
        {
            var model = this.tripsService.GetById<TripViewModel>(id);

            return this.View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = this.tripsService.GetEditViewModel(id);

            model.CityItems = this.citiesService.GetAllCities();
            model.CategoriesItems = this.categoriesService.GetAllLocationCategories();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTripViewModel input)
        {
            await this.tripsService.UpdateAsync(input);
            var model = this.tripsService.GetById<TripViewModel>(input.Id);
            return this.RedirectToAction("GetById", model);
        }

        public async Task<IActionResult> Delete(int id)
        {

            await this.tripsService.DeleteAsync(id);
            return this.RedirectToAction("All");
        }
    }
}
