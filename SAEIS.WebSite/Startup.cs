﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using SAEIS.Data;
using SAEON.Logs;
using System;
using System.Globalization;
using System.IO;

namespace SAEIS.WebSite
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            using (Logging.MethodCall(GetType()))
            {
                try
                {
                    Logging.Information("Configuring services");
                    Configuration = configuration;
                    Environment = environment;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            using (Logging.MethodCall(GetType()))
            {
                try
                {
                    Logging.Information("Configuring services");
                    if (!Environment.IsDevelopment())
                    {
                        services.AddHttpsRedirection(options =>
                        {
                            options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
                            options.HttpsPort = 443;
                        });
                    }
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

                    services.AddMvc();
                    services.AddCors();

                    IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
                    services.AddSingleton<IFileProvider>(physicalProvider);
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (Logging.MethodCall(GetType()))
            {
                try
                {
                    //Logging.Information("ContentRoot: {contentRoot} WebRoot: {webRoot}", env.ContentRootPath, env.WebRootPath);
                    bool useHTTPS = Configuration.GetValue<bool>("UseHTTPS");
                    Logging.Information("Development: {IsDevelopment} HTTPS: {HTTPS}", env.IsDevelopment(), useHTTPS);
                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                        app.UseDatabaseErrorPage();
                        //app.UseBrowserLink();
                    }
                    else
                    {
                        if (useHTTPS)
                        {
                            app.UseHttpsRedirection();
                        }
                        app.UseExceptionHandler("/Home/Error");
                    }
                    app.UseCors();

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

                    app.UseRouting();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                    });
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