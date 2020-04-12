using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeopleAnalysis.Extensions;
using PeopleAnalysis.Models;
using PeopleAnalysis.Models.Configuration;
using PeopleAnalysis.Services;
using PeopleAnalysis.Services.APIs;

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
            services.AddDbContext<DatabaseContext>(x => x.UseNpgsql(Configuration.GetConnectionString("Default")).UseLazyLoadingProxies(), ServiceLifetime.Scoped);
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

            services.AddScoped<ApisManager>();
            services.AddSingleton(Configuration.Get<KeysConfiguration>());
            services.AddScoped<AnaliticService>();
            services.AddScoped<IAnaliticAIService, AnaliticAIService>();
            services.AddScoped<VkSocialApi>();
            services.AddHostedService<RabbitMQService>();
            services.AddScoped<ISender, RabbitMQClient>();
            services.AddSingleton<IAIService, TensorService>();
            services.AddSingleton<ColorService>();
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
