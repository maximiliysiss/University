using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography.Services
{
    public class NinjectDatabaseContainer : NinjectModule
    {
        private readonly string connectionString;

        public NinjectDatabaseContainer()
        {
            connectionString = ConfigurationManager.ConnectionStrings[1].ConnectionString;
        }

        public override void Load()
        {
            this.Bind<IDatabaseContext>().To<DatabaseContext>().WithConstructorArgument("connectionString", connectionString);
        }
    }
}
