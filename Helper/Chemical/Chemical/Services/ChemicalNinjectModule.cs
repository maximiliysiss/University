using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chemical.Services
{
    public class ChemicalModules
    {
        private readonly StandardKernel standardKernel;

        public ChemicalModules()
        {
            standardKernel = new StandardKernel();
            standardKernel.Load(Assembly.GetExecutingAssembly());
        }

        public T Resolve<T>() => standardKernel.Get<T>();

        public void AddDbContext<T>(string dbString) where T : DbContext => standardKernel.Bind<T>().To<T>().WithConstructorArgument(typeof(string), dbString);
    }

    public class ChemicalNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoginService>().To<LoginService>();
        }
    }
}
