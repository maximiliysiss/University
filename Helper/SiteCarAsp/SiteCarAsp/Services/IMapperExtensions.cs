using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SiteCarAsp.Models;
using SiteCarAsp.ViewModels;

namespace SiteCarAsp.Services
{
    public static class IMapperExtensions
    {
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, Mapper>(x =>
            {
                MapperConfiguration mapperConfiguration = new MapperConfiguration(x =>
                {
                    x.CreateMap<TestDriveViewModel, TestDrive>();
                });
                return new Mapper(mapperConfiguration);
            });
        }
    }
}
