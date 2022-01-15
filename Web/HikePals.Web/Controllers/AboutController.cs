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

            SmtpClient client = new SmtpClient("asdas", 80);
            var message = new MailMessage();
            client.SendMailAsync(message);

            return this.View();
        }
    }
}