namespace HikePals.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using HikePals.Services.Data;
    using HikePals.Services.Data.Contracts;
    using HikePals.Web.ViewModels.Trips;
    using Microsoft.AspNetCore.Mvc;

    public class TripsController : AdministrationController
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
            var model = this.tripsService.GetAllWithDeleted();

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
            return this.RedirectToAction("Create", nameof(Web.Controllers.TripsController));
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
            await this.tripsService.RestoreAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
