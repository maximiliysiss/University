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
        /// <summary>
        /// IOC контейнер
        /// </summary>
        private static ProductionModule productionModule;
        public static ProductionModule ProductionModule => productionModule;
        /// <summary>
        /// БД
        /// </summary>
        public static DatabaseContext Db => productionModule.Resolve<DatabaseContext>();
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public static User user;
        /// <summary>
        /// Конфигурация
        /// </summary>
        private readonly IConfigurationRoot configuration;

        public App()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
            productionModule = new ProductionModule();
            productionModule.AddDbContext<DatabaseContext>(configuration.GetConnectionString("Default"));
        }
    }
}
