﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HikePals.Services.Data
{
    public interface ITransportService
    {
        IEnumerable<SelectListItem> GetAllTransportTypes();
    }
}
