using BeatifulStudio.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatifulStudio.DataLayout.Models
{
    public enum Role
    {
        User,
        Admin,
        Master
    }
    public class User
    {
        public int ID { get; set; }
        public Role Role { get; set; }
        public string Login { get; set; }
        private string password;
        public string Password
        {
            get => password;
            set => password = value;
        }
        public bool IsFree(DateTime dateTime)
        {
            return Schedule.ElementAt((int)dateTime.DayOfWeek).ToString() == "1";
        }

        [NotMapped]
        public string PasswordString
        {
            get { return password; }
            set { password = CryptService.CreateMD5(value); }
        }
        public string Schedule { get; set; } = "1111111";

        public List<UsersService> UsersServices { get; set; }
        public List<UsersService> MastersService { get; set; }
    }
}
