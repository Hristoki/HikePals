namespace HikePals.Services.Data
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class MailService : IMailService
    {
        private IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = this._configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("hikepals@abv.bg", "Hike Pals");
            var to = new EmailAddress(toEmail, "Example User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
