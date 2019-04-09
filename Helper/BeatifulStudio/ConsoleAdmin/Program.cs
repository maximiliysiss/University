using BeatifulStudio.DataLayout;
using BeatifulStudio.DataLayout.Models;
using BeatifulStudio.Services;
using System;
using System.Linq;

namespace ConsoleAdmin
{
    class Program
    {
        public static DatabaseContext DatabaseContext { get; set; } = new DatabaseContext();
        static void AddUser()
        {
            Console.WriteLine(@"Enter user name && password && role:");
            var info = Console.ReadLine().Split(' ');
            if (info.Length != 3 && info.Any(string.IsNullOrEmpty))
            {
                Console.WriteLine("Incorrect data");
                return;
            }

            if (Enum.TryParse(info[3], true, out Role res))
            {

                DatabaseContext.Users.Add(new User
                {
                    Login = info[0],
                    Password = CryptService.CreateMD5(info[1]),
                    Role = res
                });
                DatabaseContext.SaveChanges();
                Console.WriteLine("Success");
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter your choice:\n1) Create user\n");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    switch (value)
                    {
                        case 1:
                            AddUser();
                            break;
                    }
                }
            }
        }
    }
}
