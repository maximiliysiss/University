using Chemical.Services;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace Chemical
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ChemicalModules chemicalModules;
        public static ChemicalModules ChemicalModules => chemicalModules;

        private readonly IConfigurationRoot configuration;

        public App()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
            chemicalModules = new ChemicalModules();
            chemicalModules.AddDbContext<DatabaseContext>(configuration.GetConnectionString("Default"));
        }
    }
}
