using Microsoft.Extensions.DependencyInjection;
using PeopleAnalysis.Models.Configuration;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

namespace PeopleAnalysis.Extensions
{
    public static class ApplicationExtension
    {
        public static void AddApi(this IServiceCollection services, KeysConfiguration keysConfiguration)
        {
            services.AddSingleton<IVkApi>(sp =>
            {
                var api = new VkApi();
                api.Authorize(new ApiAuthParams { AccessToken = keysConfiguration["vk"].Key });
                return api;
            });
        }
    }
}
