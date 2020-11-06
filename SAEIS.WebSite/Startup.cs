using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using SAEIS.WebSite.Data;
using SAEON.Logs;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace SAEIS.WebSite
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            using (SAEONLogs.MethodCall(GetType()))
            {
                try
                {
                    Configuration = configuration;
                    Environment = environment;
                    SAEONLogs.Information("AppInsights: {Key}", configuration["ApplicationInsights:InstrumentationKey"]);
                }
                catch (Exception ex)
                {
                    SAEONLogs.Exception(ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            using (SAEONLogs.MethodCall(GetType()))
            {
                try
                {
                    SAEONLogs.Information("Configuring services");
                    services.AddApplicationInsightsTelemetry();
                    services.Configure<CookiePolicyOptions>(options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });
                    services.ConfigureApplicationCookie(options =>
                    {
                        options.Cookie.SameSite = SameSiteMode.None;
                    });
                    services.AddResponseCaching();
                    services.AddResponseCompression();

                    var connectionString = Configuration.GetConnectionString("SAEIS");
                    var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
                    services.AddDbContext<SAEISContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly).EnableRetryOnFailure()));

                    services.AddControllersWithViews();
                    services.AddRazorPages();

                    IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
                    services.AddSingleton<IFileProvider>(physicalProvider);
                }
                catch (Exception ex)
                {
                    SAEONLogs.Exception(ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (SAEONLogs.MethodCall(GetType()))
            {
                try
                {
                    //SAEONLogs.Information("ContentRoot: {contentRoot} WebRoot: {webRoot}", env.ContentRootPath, env.WebRootPath);
                    SAEONLogs.Information("Development: {IsDevelopment}", env.IsDevelopment());
                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                        app.UseDatabaseErrorPage();
                        //app.UseBrowserLink();
                    }
                    else
                    {
                        app.UseExceptionHandler("/Home/Error");
                    }
                    app.UseHttpsRedirection();

                    app.UseStaticFiles();
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                        RequestPath = "/node_modules",
                    });
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "Archive")),
                        RequestPath = "/Archive",
                    });
                    app.UseCookiePolicy();
                    app.UseResponseCaching();
                    app.UseResponseCompression();
                    var supportedCultures = new[]
                    {
                        new CultureInfo("en-GB")
                    };

                    app.UseRequestLocalization(new RequestLocalizationOptions
                    {
                        DefaultRequestCulture = new RequestCulture("en-GB"),
                        // Formatting numbers, dates, etc.
                        SupportedCultures = supportedCultures,
                        // UI strings that we have localized.
                        SupportedUICultures = supportedCultures
                    });

                    app.UseRouting();
                    app.UseAuthentication();
                    app.UseAuthorization();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
                }
                catch (Exception ex)
                {
                    SAEONLogs.Exception(ex);
                    throw;
                }
            }
        }
    }
}