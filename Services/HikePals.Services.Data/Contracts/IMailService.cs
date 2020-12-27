namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMailService
    {
        Task SendResetEmailPasswordAsync(string toEmail, string subject, string content);

        Task SendEmailAsync(string toEmail, string subject, string content);

        Task SendContactFormEmailAsync(string sentBy, string subject, string content, string name);

    }
}
