using Bank.Models;
using Bank.Services;
using Bank.Settings;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace Bank
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Настройки для приложения
        /// </summary>
        private static SettingsInfo settingsInfo;
        private static BankModules bankModules;
        /// <summary>
        /// Контейнер
        /// </summary>
        public static BankModules BankModules => bankModules;
        public static SettingsInfo SettingsInfo => settingsInfo;
        /// <summary>
        /// БД
        /// </summary>
        public static DatabaseContext Db => bankModules.Resolve<DatabaseContext>();
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
            settingsInfo = configuration.GetSection("SettingsInfo").Get<SettingsInfo>();
            bankModules = new BankModules();
            bankModules.AddDbContext<DatabaseContext>(configuration.GetConnectionString("Default"));
        }
    }
}
