namespace HikePals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HikePals.Services.Data;
    using HikePals.Web.ViewModels.Rating;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/[controller]")]
    public class RatingsController : BaseController
    {
        private readonly IRatingsService ratingsService;

        public RatingsController(IRatingsService ratingsService)
        {
            this.ratingsService = ratingsService;
        }

        [HttpPost]
        public async Task<ActionResult<RatingResponseModel>> Vote(RatingInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.ratingsService.VoteAsync(userId, input.TripId, input.Value);
            var averageRating = this.ratingsService.GetAverageRating(input.TripId);

            return new RatingResponseModel {AverageRating = averageRating, UserVoteValue = input.Value };
        }
    }
}
