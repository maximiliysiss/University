using Garage.Models;
using Garage.Services;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace Garage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static GarageModule productionModule;
        public static GarageModule ProductionModule => productionModule;
        public static DatabaseContext Db => productionModule.Resolve<DatabaseContext>();
        public static User user;

        private static readonly IConfigurationRoot configuration;
        public static IConfigurationRoot Configuration => configuration;

        static App() => configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();

        public App()
        {
            productionModule = new GarageModule();
            productionModule.AddDbContext<DatabaseContext>(configuration.GetConnectionString("Default"));
        }
    }
}
