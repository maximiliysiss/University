using System;
using System.Collections.Generic;
using System.Text;

namespace Zimin_Maxim_PRI_116_Lab_01.Model
{
    /// <summary>
    /// Interface for Man
    /// </summary>
    public interface IMan
    {
        void Talk();
        void Run();
        void Stop();
        void Kill();
        void Eat();
        void Harm();
    }

    /// <summary>
    /// 
    /// </summary>
    public class Man : IMan
    {
        public bool IsAlive => health > 0;
        public bool IsRun { get; set; } = false;
        public string Name { get; set; }
        private int health;
        public int Health
        {
            get => health;
            set
            {
                health += value;
                if (health <= 0)
                {
                    health = 0;
                    Console.WriteLine($"{Name} is dead");
                }
            }
        }

        public void Talk()
        {
            if (!IsAlive)
            {
                Console.WriteLine($"{Name} is dead");
                return;
            }

            Random random = new Random(Environment.TickCount);
            switch (random.Next() % 2)
            {
                case 0:
                    Console.WriteLine($"Hello! My name is {Name}");
                    break;
                case 1:
                    Console.WriteLine($"Hello! My health is {Health}");
                    break;
            }
        }

        public void Run()
        {
            if (IsRun)
                Console.WriteLine("Running yet");
            else
                Console.WriteLine("Start run");
            IsRun = true;
        }

        public void Stop()
        {
            if (IsRun)
                Console.WriteLine("Stop run");
            else
                Console.WriteLine($"{Name} already stop");
            IsRun = false;
        }

        public void Kill()
        {
            if (IsAlive)
                Console.WriteLine($"{Name} killed");
            else
                Console.WriteLine($"{Name} already killed");
            health = 0;
        }

        public void Harm()
        {
            if (!IsAlive)
            {
                Console.WriteLine($"{Name} is dead");
                return;
            }
            Health = -20;
        }

        public void Eat()
        {
            if (!IsAlive)
            {
                Console.WriteLine($"{Name} is dead");
                return;
            }
            // Add health by food
            Health = 20;
        }

        public bool IsLife { get; set; } = true;

        public Man(string name)
        {
            Console.WriteLine($"Create {name}");
            Name = name;
            health = 100;
        }

    }
}
