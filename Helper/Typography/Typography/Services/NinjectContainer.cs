using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography.Services
{
    /// <summary>
    /// Контейнер для Базы данных
    /// </summary>
    public class NinjectDatabaseContainer : NinjectModule
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Конструктор
        /// </summary>
        public NinjectDatabaseContainer()
        {
            connectionString = ConfigurationManager.ConnectionStrings[1].ConnectionString;
        }

        /// <summary>
        /// Установка зависимостей
        /// </summary>
        public override void Load()
        {
            this.Bind<IDatabaseContext>().To<DatabaseContext>().WithConstructorArgument("connectionString", connectionString);
        }
    }
}
