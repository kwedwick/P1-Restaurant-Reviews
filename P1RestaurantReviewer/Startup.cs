using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P1RestaurantReviewer.Data;
using P1RestaurantReviewer.DataAccess;
using P1RestaurantReviewer.DataAccess.Entities;
using P1RestaurantReviewer.Domain;
using P1RestaurantReviewer.Models.Configuration;
using P1RestaurantReviewer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace P1RestaurantReviewer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // the repos
            //unit of work pattern
            services.AddScoped<IRestaurantRepo, RestaurantRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IReviewRepo, ReviewRepo>();

            //GoogleSMPT authorization
            services.Configure<GoogleSMTPDetails>(Configuration.GetSection("GoogleSMTP"));

            // need to add connection string to get database:
            services.AddDbContext<restaurantreviewerContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("p0restreviewerdb"));
                options.LogTo(Console.WriteLine);
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("p0restreviewerdb"));
                options.LogTo(Console.WriteLine);
            });
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });


            services.AddTransient<IEmailSender, EmailSender>();

            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
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
    }
}
