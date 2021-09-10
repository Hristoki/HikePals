using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(HikePals.Web.Areas.Identity.IdentityHostingStartup))]

namespace HikePals.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
