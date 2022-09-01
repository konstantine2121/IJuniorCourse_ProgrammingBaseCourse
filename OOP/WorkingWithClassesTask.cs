using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{
    /// <summary>
    /// ///<br/>
    ///Задача<br/>
    ///<br/>
    ///Создать класс игрока, с полями, содержащими информацию об игроке и методом, который выводит информацию на экран.<br/>
    ///<br/>
    ///В классе обязательно должен быть конструктор<br/>
    /// </summary>
    class WorkingWithClassesTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            var player = new Player("Bob", 100);
            player.PrintInfo();

            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private class Player
        {
            public string Name { get; private set; }

            public int Health { get; private set; }

            /// <summary>
            /// Создаем игрока.
            /// </summary>
            /// <param name="name">Имя.</param>
            /// <param name="health">Здоровье.</param>
            /// <exception cref="ArgumentException"></exception>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public Player (string name, int health)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException(nameof(name));
                }

                if (health < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(health));
                }

                Name = name;
                Health= health;
            }

            public void PrintInfo()
            {
                Console.WriteLine("Информаци об игроке.");
                Console.WriteLine("Имя: {0}\nОчки здоровья: {1}",Name, Health);
            }
        }
    }
}
