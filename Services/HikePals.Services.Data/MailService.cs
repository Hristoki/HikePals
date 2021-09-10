namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class MailService : IMailService
    {
        private IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendContactFormEmailAsync(string sentByEmail, string subject, string content, string name)
        {
            var apiKey = this.configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("hikepals@abv.bg", name);
            var to = from;
            var contentPackage = new StringBuilder();
            contentPackage.AppendLine($"From: {name} - {sentByEmail}");
            contentPackage.AppendLine(Environment.NewLine);
            contentPackage.AppendLine(content);
            var subjectFromUser = $"Contact Form Subject: {subject}";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, contentPackage.ToString(), contentPackage.ToString());
            var response = await client.SendEmailAsync(msg);
        }

        public Task SendEmailAsync(string toEmail, string subject, string content)
        {
            throw new NotImplementedException();
        }

        public async Task SendResetEmailPasswordAsync(string toEmail, string subject, string content)
        {
            var apiKey = this.configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("hikepals@abv.bg", "Hike Pals");
            var to = new EmailAddress(toEmail, "Example User");

            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
