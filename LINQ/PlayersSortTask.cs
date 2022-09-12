using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IJuniorCourse_ProgrammingBaseCourse.LINQ
{
    /// <summary>
    /// Задача<br/>
    /// <br/>
    ///У нас есть список всех игроков(минимум 10). У каждого игрока есть поля: имя, уровень, сила. <br/>
    ///Требуется написать запрос для определения топ 3 игроков по уровню и топ 3 игроков по силе, после чего вывести каждый топ.<br/>
    ///2 запроса получится. 
    /// </summary>
    class PlayersSortTask : IRunnable
    {
        private List<PlayerRecord> _players;

        #region IRunnable Implementation

        public void Run()
        {
            _players = new PlayerRecordCreator().Create();

            ShowTopsByLevel();

            ShowTopsByPower();

            Console.WriteLine("\nНажмите Enter чтобы выйти из программы.");
            Console.ReadLine();
        }

        #endregion IRunnable Implementation

        private void ShowTopsByLevel()
        {
            const int numberOfTops = 3;

            var sorted = _players.OrderByDescending(record => record.Level).Take(numberOfTops);

            Console.WriteLine("Топ три игрока по уровню.");
            PrintRecords(sorted);

            Console.WriteLine();
        }

        private void ShowTopsByPower()
        {
            const int numberOfTops = 3;

            var sorted = _players.OrderByDescending(record => record.Power).Take(numberOfTops);

            Console.WriteLine("Топ три игрока по силе.");
            PrintRecords(sorted);

            Console.WriteLine();
        }

        private void PrintRecords(IEnumerable<PlayerRecord> records)
        {
            const string format = "{0, 20}  {1, 8}  {2, 8}";
            ConsoleOutputMethods.Info(string.Format(format, "Имя", "Уровень", "Сила"));

            foreach (var record in records)
            {
                Console.WriteLine(format, record.FullName, record.Level, record.Power);
            }
        }

        #region Private Classes

        private class PlayerRecord
        {
            public PlayerRecord(string fullName, int level, int power)
            {
                FullName = fullName;
                Level = level;
                Power = power;
            }

            public string FullName { get; private set; }

            public int Level { get; private set; }

            public int Power { get; private set; }
        }

        #region Creators

        private interface ICreator<T>
        {
            T Create();
        }

        private class RandomContainer
        {
            protected readonly Random Rand = new Random();
        }

        private class PlayerRecordCreator : RandomContainer, ICreator<List<PlayerRecord>>
        {
            private const string NameTemplate = "Nickname ";

            private const int MinLevel = 1;
            private const int MaxLevel = 100;

            private const int MinPower = 1000;
            private const int MaxPower = 5000;

            private const int NumberOfPlayers = 20;

            public List<PlayerRecord> Create()
            {                
                return CreatePlayer();
            }

            private List<PlayerRecord> CreatePlayer()
            {
                var patients = new List<PlayerRecord>();

                for (int i = 0; i < NumberOfPlayers; i++)
                {
                    patients.Add(CreatePlayer(i));
                }

                return patients;
            }

            private PlayerRecord CreatePlayer(int index)
            {
                var name = NameTemplate + index;
                var level = Rand.Next(MinLevel, MaxLevel);
                var power = Rand.Next(MinPower, MaxPower);

                return new PlayerRecord(name, level, power);
            }
        }

        #endregion Creators

        #endregion Private Classes
    }
}
