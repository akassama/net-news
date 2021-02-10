using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetNews.Models;
using NetNews.Models.AppModels;

namespace NetNews
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Dependency injection for the DbContext, Creates an instance of employee context class
            services.AddDbContext<DBConnection>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            // Add detection services container and device resolver service.
            services.AddDetection();

            services.AddControllersWithViews();

            //Read config from apsettings
            services.Configure<SystemConfiguration>(Configuration.GetSection("systemConfiguration"));

            //runtime compilation
            services.AddControllersWithViews();

            //Registering IHttpContextAccessor and SessionManager in Startup file.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<SessionManager>();

            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //Error page handlers
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Error/E404";
                    await next();
                }
                else if (context.Response.StatusCode == 400)
                {
                    context.Request.Path = "/Error/E400";
                    await next();
                }
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseDetection();

            app.UseRouting();

            app.UseAuthorization();

            //Use session here
            app.UseSession();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "posts",
                    pattern: "Posts/{id?}",
                    defaults: new { controller = "Posts", action = "Index" });
                endpoints.MapControllerRoute(name: "category",
                    pattern: "Category/{id?}",
                    defaults: new { controller = "Category", action = "Index" });
                endpoints.MapControllerRoute(name: "search",
                    pattern: "Search/{id?}",
                    defaults: new { controller = "Search", action = "Index" });
                endpoints.MapControllerRoute(name: "tags",
                    pattern: "Tags/{id?}",
                    defaults: new { controller = "Tags", action = "Index" });
                endpoints.MapControllerRoute(name: "adverts",
                    pattern: "Adverts/{id?}",
                    defaults: new { controller = "Adverts", action = "Index" });
                endpoints.MapControllerRoute(name: "jobs",
                    pattern: "Jobs/{id?}",
                    defaults: new { controller = "Jobs", action = "Index" });
                endpoints.MapControllerRoute(name: "authors",
                   pattern: "Authors/{id?}",
                   defaults: new { controller = "Authors", action = "Index" });
                endpoints.MapControllerRoute(name: "latest",
                   pattern: "LatestNews/{id?}",
                   defaults: new { controller = "LatestNews", action = "Index" });
                endpoints.MapControllerRoute(name: "all",
                   pattern: "AllNews/{id?}",
                   defaults: new { controller = "AllNews", action = "Index" });
            });
        }
    }
}
