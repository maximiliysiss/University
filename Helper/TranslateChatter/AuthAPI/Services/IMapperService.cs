using AuthAPI.Models.Controller;
using AuthAPI.Models.Database;
using AutoMapper;
using System;

namespace AuthAPI.Services
{
    /// <summary>
    /// Сервис маппер
    /// </summary>
    public interface IMapperService
    {
        /// <summary>
        /// Мапить данные
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="fromMap"></param>
        /// <returns></returns>
        R Map<R>(object fromMap);
    }

    public class AutoMapperService : IMapperService
    {
        private readonly Mapper mapper;

        public AutoMapperService()
        {
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<User, UserModel>();
                x.CreateMap<Role, RoleModel>();
                x.CreateMap<Language, LanguageModel>();
            });

            mapper = new Mapper(config);
        }

        public R Map<R>(object fromMap) => mapper.Map<R>(fromMap);
    }
}
