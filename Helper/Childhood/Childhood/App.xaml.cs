using Childhood.Models;
using Childhood.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Childhood
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ChildhoodModule childhoodModule;
        public static ChildhoodModule ChildhoodModule => childhoodModule;
        public static DatabaseContext Db => childhoodModule.Resolve<DatabaseContext>();
        public static User user;

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
