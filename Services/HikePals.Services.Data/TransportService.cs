namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using HikePals.Data.Common.Models;
    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TransportService : ITransportService
    {
        private readonly IRepository<Transport> transportRepository;

        public TransportService(IRepository<Transport> transportRepository)
        {
            this.transportRepository = transportRepository;
        }

        public IEnumerable<SelectListItem> GetAllTransportTypes()
        {
            return this.transportRepository
                .All()
                .Select(x => new { x.Id, x.Name })
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
        }
    }
}
