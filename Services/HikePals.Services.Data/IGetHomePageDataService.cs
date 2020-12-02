
using HikePals.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text;

namespace HikePals.Services.Data
{
    public interface IGetHomePageDataService
    {
         IndexViewModel GetCounts();

    }
}
