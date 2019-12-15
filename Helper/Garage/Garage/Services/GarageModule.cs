using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Garage.Services
{
    public class GarageModule
    {
        /// <summary>
        /// Контейнер
        /// </summary>
        private readonly StandardKernel standardKernel;

        /// <summary>
        /// Инициализация
        /// </summary>
        public GarageModule()
        {
            standardKernel = new StandardKernel();
            standardKernel.Load(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Получить данные из контейнера
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() => standardKernel.Get<T>();

        /// <summary>
        /// Добавить контекст
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbString"></param>
        public void AddDbContext<T>(string dbString) where T : DbContext => standardKernel.Bind<T>().To<T>().WithConstructorArgument(typeof(string), dbString);
    }

    /// <summary>
    /// Инициализация контейнера
    /// </summary>
    public class GarageNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoginService>().To<LoginService>();
            Bind<UserWindowService>().To<UserWindowService>();
        }
    }
}
