using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.OOP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Реализовать базу данных игроков и методы для работы с ней.<br/>
    ///У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).<br/>
    ///Реализовать возможность добавления игрока, бана игрока по уникальный номеру, разбана игрока по уникальный номеру и удаление игрока.<br/>
    ///Создание самой БД не требуется, задание выполняется инструментами, которые вы уже изучили в рамках курса. Но нужен класс, который содержит игроков и её можно назвать "База данных".
    /// </summary>
    class DatabaseGenericsTest : IRunnable
    {
        private const string PlayerRecordPrintFormat = "{0, 32} {1, 10} {2, 4} {3}";

        #region IRunnable Implementation

        public void Run()
        {
            int banLevelCondition = 80;
            string removeNameCondition = "p1";

            var database = new PlayersDatabase ();

            database.Insert(new Player(string.Empty, "p1", 1));
            database.Insert(new Player(string.Empty, "p2", 10));
            database.Insert(new Player(string.Empty, "p3", 100));
            database.Insert("p4", 120, false);

            PrintDatabaseInfo(database);

            var playersToBan = database.SelectAllRecords()
                .Where(player => player.Level > banLevelCondition);

            foreach (var player in playersToBan)
            {
                database.Ban(player.Id);
            }

            PrintDatabaseInfo(database);

            var playersToRemove = database.SelectAllRecords()
                .Where(player => player.Name == removeNameCondition).ToList();

            foreach (var player in playersToRemove)
            {
                database.Delete(player.Id);
            }

            PrintDatabaseInfo(database);

            var playersToUnban = database.SelectAllRecords()
                .Where(player => player.Level > banLevelCondition);

            foreach (var player in playersToBan)
            {
                database.Unban(player.Id);
            }

            PrintDatabaseInfo(database);

            Console.ReadKey();
        }

        private void PrintDatabaseInfo(Database<Player> database)
        {
            var records = database.SelectAllRecords();

            Console.WriteLine("Вывод информации из базы.");
            Console.WriteLine(PlayerRecordPrintFormat, "Id", "Имя", "lvl", "Забанен");

            foreach(var record in records)
            {
                PrintPlayerInfo(record);
            }

            Console.WriteLine();
        }

        private void PrintPlayerInfo(Player player)
        {   
            if (player == null)
            {
                Console.WriteLine("Запись пуста.");
            }
            else
            {
                Console.WriteLine(PlayerRecordPrintFormat, player.Id, player.Name, player.Level, player.Banned);
            }
        }

        #endregion IRunnable Implementation

        /// <summary>
        /// DTO
        /// </summary>
        private class Player : IIdContainer, IClonable<Player>
        {
            public Player (string id, string name, int level, bool banned = false)
            {
                Id = id;
                Name = name;
                Level = level;
                Banned = banned;
            }

            public string Id { get; set; }

            public string Name { get; set; }

            public int Level { get; set; }

            public bool Banned { get; set; }

            public Player Clone()
            {
                return new Player(Id, Name, Level, Banned);
            }
        }

        private class Database<T> where T : IClonable<T>, IIdContainer
        {
            protected readonly List<T> Records = new List<T>();

            public void Insert(T record)
            {
                if (record == null)
                {
                    throw new ArgumentNullException(nameof(record));
                }

                string guid = Guid.NewGuid().ToString("N");

                record.Id = guid;

                Records.Add(record);
            }

            public void Delete(string playerId)
            {
                var player = Records.Find(record => record.Id == playerId);

                if (player != null)
                {
                    Records.Remove(player);
                }
            }

            public IReadOnlyCollection<T> SelectAllRecords()
            {
                //Представим, что это датасет. Датасет по умолчанию создает копии записей из БД.
                var result = new List<T>();

                result = Records
                    .Select(record => CloneRecord(record))
                    .ToList();

                return result;
            }

            private T CloneRecord(T record)
            {
                if (record == null)
                {
                    throw new ArgumentNullException(nameof(record));
                }
                var result = record.Clone();

                return result;
            }
        }

        private class PlayersDatabase : Database<Player>
        {
            public void Insert(string name, int level, bool banned = false)
            {
                const int minLevel = 1;

                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (level < minLevel)
                {
                    throw new ArgumentOutOfRangeException(nameof(level));
                }

                string guid = Guid.NewGuid().ToString("N");

                Player player = new Player(guid, name, level, banned);

                Records.Add(player);
            }

            public void Ban(string playerId)
            {
                var player = Records.Find(record => record.Id == playerId);

                if (player != null)
                {
                    player.Banned = true;
                }
            }

            public void Unban(string playerId)
            {
                var player = Records.Find(record => record.Id == playerId);

                if (player != null)
                {
                    player.Banned = false;
                }
            }
        }
    }
        
}
