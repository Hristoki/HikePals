namespace HikePals.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public class SettingsService : ISettingsService
    {
        private readonly IRepository<Setting> settingsRepository;

        public SettingsService(IRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All().To<T>().ToList();
        }
    }
}
