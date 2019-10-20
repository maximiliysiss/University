﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolService.Models
{
    public enum UserType
    {
        Admin,
        Social, // Соц педагог
        KnowledgeTeacher, // Зауч по учебному процессу
        JobTeacher, // Зауч по рабочему процессу
        Teacher,
        Student
    }

    public class User
    {
        public int ID { get; set; }
        [NotMapped]
        [JsonProperty("Login")]
        public string LoginJson
        {
            set => Login = value;
        }
        [JsonIgnore]
        public string Login { get; set; }
        [NotMapped]
        [JsonProperty("PasswordHash")]
        public string PasswordJson
        {
            set => PasswordHash = value;
        }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public DateTime Birthday { get; set; }
        public UserType UserType { get; set; }
        [JsonIgnore]
        public string Token { get; set; }

        [JsonIgnoreAttribute]
        [NotMapped]
        public ClaimsIdentity ClaimsIdentity
        {
            get
            {
                var claims = new[] {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, UserType.ToString()),
                };
                return new ClaimsIdentity(claims);
            }
        }
    }
}
