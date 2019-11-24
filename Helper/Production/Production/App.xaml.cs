using Microsoft.Extensions.Configuration;
using Production.Models;
using Production.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Production
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ProductionModule productionModule;
        public static ProductionModule ProductionModule => productionModule;
        public static DatabaseContext Db => productionModule.Resolve<DatabaseContext>();
        public static User user;

        private readonly IConfigurationRoot configuration;

        public App()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
            productionModule = new ProductionModule();
            productionModule.AddDbContext<DatabaseContext>(configuration.GetConnectionString("Default"));
        }
    }
}
