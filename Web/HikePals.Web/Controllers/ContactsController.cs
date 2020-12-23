namespace HikePals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HikePals.Data.Models;
    using HikePals.Services.Data;
    using HikePals.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : BaseController
    {
        private readonly IMailService mailService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContactsController(IMailService mailService, UserManager<ApplicationUser> userManager)
        {
            this.mailService = mailService;
            this.userManager = userManager;
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact(ContactFormInputViewModel input)
        {

            if (!this.ModelState.IsValid)
            {

                return this.View(input);
            }
            this.mailService.SendContactFormEmailAsync(input.Email, input.Subject, input.Content, input.Name);
            return this.View("ThankYou");
        }
    }
}
