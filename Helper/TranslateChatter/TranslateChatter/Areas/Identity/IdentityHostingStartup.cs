using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TranslateChatter.Areas.Identity.IdentityHostingStartup))]
namespace TranslateChatter.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}