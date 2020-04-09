using Microsoft.AspNetCore.Builder;
using PeopleAnalysis.Models.Configuration;
using PeopleAnalysis.Services;
using PeopleAnalysis.Services.APIs;
using System;
using VkNet.Model;

namespace PeopleAnalysis.Extensions
{
    public static class ApplicationExtension
    {
        public static T GetService<T>(this IServiceProvider serviceProvider) => (T)serviceProvider.GetService(typeof(T));
    }
}
