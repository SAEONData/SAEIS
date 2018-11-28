using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SAEIS.Data;
using SAEON.Logs;
using System;
using System.Globalization;
using System.IO;

namespace SAEIS.WebSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            using (Logging.MethodCall(GetType()))
            {
                try
                {
                    Logging.Information("Configuring services");
                    Configuration = configuration;
                    Logging
                        .CreateConfiguration("Logs/SAEIS.WebSite.txt", configuration)
                        .Create();
                    Logging.Information("AppInsights: {Key}",configuration["ApplicationInsights:InstrumentationKey"]);
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            using (Logging.MethodCall(GetType()))
            {
                try
                {
                    Logging.Information("Configuring services");
                    services.AddApplicationInsightsTelemetry(Configuration);
                    services.Configure<CookiePolicyOptions>(options =>
                    {
                        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

                    var connectionString = Configuration.GetConnectionString("SAEIS");
                    services.AddDbContext<SAEISDbContext>(options => options.UseSqlServer(connectionString));
                    //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
                    //services.AddDbContext<SAEONDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly).EnableRetryOnFailure()));

                    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                    services.AddCors();
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (Logging.MethodCall(GetType()))
            {
                try
                {
                    Logging.Verbose("Configure: {IsDevelopment}", env.IsDevelopment());
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
                    app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

                    var cachePeriod = env.IsDevelopment() ? "600" : "604800";
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        OnPrepareResponse = ctx =>
                        {
                            ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                        }
                    });
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                        RequestPath = "/node_modules",
                        OnPrepareResponse = ctx =>
                        {
                            ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                        }

                    });
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "Archive")),
                        RequestPath = "/Archive",
                        OnPrepareResponse = ctx =>
                        {
                            ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                        }
                    });
                    app.UseCookiePolicy();
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

                    //app.UseMvc(routes =>
                    //{
                    //    routes.MapRoute(
                    //        name: "default",
                    //        template: "{controller=Home}/{action=Index}/{id?}");
                    //});
                    app.UseMvcWithDefaultRoute();
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }
    }
}