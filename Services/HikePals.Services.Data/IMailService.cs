namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}
