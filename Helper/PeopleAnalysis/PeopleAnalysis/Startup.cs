using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeopleAnalysis.ApplicationAPI;
using PeopleAnalysis.Models;
using PeopleAnalysis.Models.Configuration;
using PeopleAnalysis.Services;

namespace PeopleAnalysis
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
            services.AddDbContext<AuthContext>(x => x.UseNpgsql(Configuration.GetConnectionString("Default")), ServiceLifetime.Scoped);

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AuthContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Auth/Login";
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton(Configuration.Get<KeysConfiguration>());
            services.AddScoped<ISender, RabbitMQClient>();
            services.AddSingleton<ColorService>();

            services.AddSingleton<IApplicationAPIClient, ApplicationAPIClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            AuthContextInitializer.RolesInit(roleManager);
            AuthContextInitializer.UsersInits(userManager);
        }
    }
}
