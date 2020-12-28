namespace HikePals.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using HikePals.Web.ViewModels.Chat;

    public interface IChatService
    {
        Task SaveMessageAsync(MessageResponseModel message);

        IEnumerable<MessageResponseModel> GetChatHistory();
    }
}
