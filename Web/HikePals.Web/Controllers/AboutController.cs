namespace HikePals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}