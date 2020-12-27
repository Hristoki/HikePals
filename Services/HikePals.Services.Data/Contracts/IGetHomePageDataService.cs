namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HikePals.Web.ViewModels.Home;

    public interface IGetHomePageDataService
    {
         IndexViewModel GetCounts();
    }
}
