using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zimin_Maxim_PRI_116_Lab_01.Model;

namespace Zimin_Maxim_PRI_116_Lab_01
{
    class Program
    {
        static void Main(string[] args)
        {
            string menu = @"
                Commands:
                create - create new man by name
                kill - kill man
                talk - man can talk some sentence
                run - start to run
                stop - stop
                eat - add health
                harm - decrease heatlh
                exit - exit
            ";

            Console.WriteLine(@"Make student Maxim Zimin from PRI-116");

            IMan man = null;
            bool isContinue = true;

            while (isContinue)
            {
                Console.WriteLine(menu);

                string command = Console.ReadLine().Trim().ToLower();
                if (command != "create" && command != "exit" && man == null)
                {
                    Console.WriteLine("man cannot found");
                    continue;
                }

                switch (command)
                {
                    case "create":
                        {
                            Console.WriteLine("Enter name:");
                            var name = Console.ReadLine();
                            man = new Man(name);
                            break;
                        }
                    case "kill":
                        man.Kill();
                        break;
                    case "talk":
                        man.Talk();
                        break;
                    case "run":
                        man.Run();
                        break;
                    case "stop":
                        man.Stop();
                        break;
                    case "eat":
                        man.Eat();
                        break;
                    case "harm":
                        man.Harm();
                        break;
                    case "exit":
                        isContinue = false;
                        break;
                }
            }
        }
    }
}