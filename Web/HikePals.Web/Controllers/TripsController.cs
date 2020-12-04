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
        private readonly ICountryService countryService;
        private readonly ILocationCategoriesService categoriesService;
        private readonly ITransportService transportService;
        private readonly ITripsService tripsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public TripsController(ICitiesService citiesService, ICountryService countryService, ILocationCategoriesService categoriesService, ITransportService transportService, ITripsService tripsService, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
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
            var viewModel = new CreateTripInputViewModel()
            {
              CityItems = this.citiesService.GetAllCities(),
              CountryItems = this.countryService.GetAllCountries(),
              TypeOfDestinationItems = this.categoriesService.GetAllLocationCategories(),
              TransportItems = this.transportService.GetAllTransportTypes(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            //TODO: Add to constants
            var imagesDirPath = $"{this.environment.WebRootPath}/images";

            var user = await this.userManager.GetUserAsync(this.User);

            await this.tripsService.AddNewTrip(input, user.Id, imagesDirPath);

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            return this.View();
        }
    }
}
