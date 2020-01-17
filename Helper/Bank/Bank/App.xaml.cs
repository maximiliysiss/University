using Bank.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Bank
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static BankModules bankModules;
        public static BankModules BankModules => bankModules;
        public static DatabaseContext Db => bankModules.Resolve<DatabaseContext>();

        private readonly IConfigurationRoot configuration;

        public App()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
            bankModules = new BankModules();
            bankModules.AddDbContext<DatabaseContext>(configuration.GetConnectionString("Default"));
        }
    }
}
