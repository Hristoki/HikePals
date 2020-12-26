namespace HikePals.Services.Data.UserServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUsersService
    {
        int GetAllUsersCount();

        //IEnumerable<T> FilterUserEntity<T>(IEnumerable<T> list, int userId);
    }
}
