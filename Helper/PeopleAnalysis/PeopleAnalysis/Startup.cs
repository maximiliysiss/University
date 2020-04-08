using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PeopleAnalysis.Extensions;
using PeopleAnalysis.Models;
using PeopleAnalysis.Models.Configuration;
using PeopleAnalysis.Services;
using System.Collections.Generic;

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
            services.AddControllersWithViews();
            services.AddDbContext<DatabaseContext>(x => x.UseNpgsql(Configuration.GetConnectionString("Default")));
            services.AddDbContext<AuthContext>(x => x.UseNpgsql(Configuration.GetConnectionString("Default")));
            services.AddDefaultIdentity<User>(o =>
            {
                o.Tokens.AuthenticatorIssuer = "http://localhost";
            }).AddEntityFrameworkStores<AuthContext>();
            services.AddRazorPages();
            services.AddApi(Configuration.Get<KeysConfiguration>());
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
