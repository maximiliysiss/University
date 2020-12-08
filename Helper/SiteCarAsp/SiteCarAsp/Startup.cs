using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SiteCarAsp.Services;
using SiteCarAsp.Services.Controller;

namespace SiteCarAsp
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
            services.AddControllersWithViews();
            services.AddSingleton<ICarsRepository>(new CarsRepository(Configuration["DbPath:Car"]));
            services.AddSingleton<ITestDriveRepository>(new TestDriveRepository(Configuration["DbPath:TestDrive"]));
            services.AddSingleton<ICreditRepository>(new CreditRepository(Configuration["DbPath:Credit"]));
            services.AddScoped<ICarService, CarsService>();
            services.AddScoped<ICreditsService, CreditsService>();
            services.AddScoped<ITestDriveService, TestDriveService>();
            services.AddSingleton<ColorService>();
            services.AddMapper();
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Car}/{action=Index}/{id?}");
            });
        }
    }
}
