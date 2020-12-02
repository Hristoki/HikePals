using HikePals.Data.Common.Repositories;
using HikePals.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HikePals.Data.Common.Models;

namespace HikePals.Services.Data
{
    public class TransportService : ITransportService
    {
        private readonly IDeletableEntityRepository<Transport> transportRepository;

        public TransportService(IDeletableEntityRepository<Transport> transportRepository)
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
