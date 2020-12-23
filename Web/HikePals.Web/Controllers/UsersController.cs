namespace HikePals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using HikePals.Data.Models;
    using HikePals.Services.Data;
    using HikePals.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IMailService mailService;

        public UsersController(UserManager<ApplicationUser> userManager, IWebHostEnvironment environment, IMailService mailService)
        {
            this.userManager = userManager;
            this.environment = environment;
            this.mailService = mailService;
        }

        public IActionResult Index()
        {
            return this.View();
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
                // Find the user by email
                var user = await this.userManager.FindByEmailAsync(model.Email);
                // If the user is found AND Email is confirmed
                if (user != null)
                {
                    // Generate the reset password token
                    var token = await this.userManager.GeneratePasswordResetTokenAsync(user);

                    // Build the password reset link
                    var passwordResetLink = this.Url.Action("ResetPassword", "Users", new { email = model.Email, token = token }, this.Request.Scheme);

                    // Log the password reset link
                    //this.logger.Log(LogLevel.Warning, passwordResetLink);

                    string docPath = $"{this.environment.WebRootPath}";

                    // Write the specified text asynchronously to a new file named "WriteTextAsync.txt".
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteTextAsync.txt")))
                    {
                        await outputFile.WriteAsync($"{LogLevel.Warning}, {passwordResetLink}");
                    }
                    var content = $"<h1>Reset your password</h1><p> You told us you forgot your password. If you really did, click here to choose a new one:</p><p><a href='{ passwordResetLink}' >Reset</a></p>";

                    //var a = $"<a href='http://localhost:53008/authentication/confirmhire?Cid=‌​{passwordResetLink}'>He‌​re</a>";

                    await this.mailService.SendEmailAsync(model.Email, "HikePals: Reset password requested!", content);

                    // Send the user to Forgot Password Confirmation view
                    return this.View("ForgotPasswordConfirmation");
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist or is not confirmed
                return this.View("ForgotPasswordConfirmation");
            }

            return this.View(model);
        }


        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                this.ModelState.AddModelError("", "Invalid password reset token");
            }
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                // Find the user by email
                var user = await this.userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await this.userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return this.View("ResetPasswordConfirmation");
                    }

                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        this.ModelState.AddModelError("", error.Description);
                    }

                    return this.View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return this.View("ResetPasswordConfirmation");
            }

            // Display validation errors if model state is not valid
            return this.View(model);
        }

    }
}
