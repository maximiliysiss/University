using Childhood.Models;
using Childhood.Services;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace Childhood
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// IOC контейнер
        /// </summary>
        private static ChildhoodModule childhoodModule;
        public static ChildhoodModule ChildhoodModule => childhoodModule;
        /// <summary>
        /// БД
        /// </summary>
        public static DatabaseContext Db => childhoodModule.Resolve<DatabaseContext>();
        public static User user;

        /// <summary>
        /// Конфигурация
        /// </summary>
        private static IConfigurationRoot configuration;
        public static IConfigurationRoot Configuration => configuration;

        public App()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
            childhoodModule = new ChildhoodModule();
            childhoodModule.AddDbContext<DatabaseContext>(configuration.GetConnectionString("Default"));
        }
    }
}
