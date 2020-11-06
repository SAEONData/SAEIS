using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAEIS.WebSite.Areas.Identity.Data;
using SAEIS.WebSite.Data;

[assembly: HostingStartup(typeof(SAEIS.WebSite.Areas.Identity.IdentityHostingStartup))]
namespace SAEIS.WebSite.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<SAEISIdentityContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("SAEISIdentity")));

                services.AddDefaultIdentity<SAEISIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SAEISIdentityContext>();
            });
        }
    }
}