﻿namespace HikePals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HikePals.Common;
    using HikePals.Data.Models;
    using HikePals.Services.Data;
    using HikePals.Web.ViewModels.Events;
    using HikePals.Web.ViewModels.Trips;
    using HikePals.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IMailService mailService;
        private readonly IEventsService eventsService;
        private readonly ITripsService tripsService;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IMailService mailService,
            IEventsService eventsService,
            ITripsService tripsService)
        {
            this.userManager = userManager;
            this.environment = environment;
            this.mailService = mailService;
            this.eventsService = eventsService;
            this.tripsService = tripsService;
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null)
            {
                return this.NotFound();
            }

            var model = new ProfileOverViewDataModel();
            model.EventsCount = this.eventsService.GetUserEventsCount(userId);
            model.TripsCount = this.tripsService.GetUserCount(userId);

            return this.View(model);
        }

        //TODO:: Move actions to respective controllers;

        [Authorize]
        public IActionResult MyTrips()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null)
            {
                return this.NotFound();
            }

            var userTrips = this.tripsService.GetAllUserTrips<TripViewModel>(userId);
            var model = new AllTripsViewModel() { Trips = userTrips };
            return this.View(model);
        }

        [Authorize]
        public IActionResult MyEvents()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null)
            {
                return this.NotFound();
            }

            var model = new AllEventAsListViewModel { Events = this.eventsService.GetAll<SingleEventListViewModel>(userId) };
            return this.View(model);
        }

        public IActionResult ForgotPassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPassWordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await this.userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = this.Url.Action("ResetPassword", "Users", new { email = model.Email, token = token }, this.Request.Scheme);

                    var content = string.Format(GlobalConstants.ResetPasswordMessage, passwordResetLink);

                    await this.mailService.SendResetEmailPasswordAsync(model.Email, "HikePals: Reset password requested!", content);

                    return this.View("ForgotPasswordConfirmation");
                }

                return this.View("ForgotPasswordConfirmation");
            }

            return this.View(model);
        }

        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid password reset token");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await this.userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return this.View("ResetPasswordConfirmation");
                    }

                    foreach (var error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return this.View(model);
                }

                return this.View("ResetPasswordConfirmation");
            }

            return this.View(model);
        }
    }
}
