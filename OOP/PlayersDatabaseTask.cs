﻿using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
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
    class PlayersDatabaseTask : IRunnable
    {
        private const string PlayerRecordPrintFormat = "{0, 32} {1, 10} {2, 4} {3}";

        #region IRunnable Implementation

        public void Run()
        {
            int banLevelCondition = 80;
            string removeNameCondition = "p1";

            var database = new PlayersDatabase();

            database.Insert(new PlayerDto(string.Empty, "p1", 1));
            database.Insert(new PlayerDto(string.Empty, "p2", 10));
            database.Insert(new PlayerDto(string.Empty, "p3", 100));
            database.Insert("p4", 120, false);

            PrintDatabaseInfo(database);

            ConsoleOutputMethods.Info("Баним всех, у кого уровень выше "+ banLevelCondition);

            var playersToBan = database.SelectAllRecords()
                .Where(player => player.Level > banLevelCondition);

            foreach (var player in playersToBan)
            {
                database.Ban(player.Id);
            }

            PrintDatabaseInfo(database);

            ConsoleOutputMethods.Info("Удаляем игрока с именем: "+ removeNameCondition);

            var playersToRemove = database.SelectAllRecords()
                .Where(player => player.Name == removeNameCondition).ToList();

            foreach (var player in playersToRemove)
            {
                database.Delete(player.Id);
            }

            PrintDatabaseInfo(database);

            ConsoleOutputMethods.Info("Разбанить всех, у кого уровень выше "+ banLevelCondition);

            var playersToUnban = database.SelectAllRecords()
                .Where(player => player.Level > banLevelCondition);

            foreach (var player in playersToBan)
            {
                database.Unban(player.Id);
            }

            PrintDatabaseInfo(database);

            Console.ReadKey();
        }

        private void PrintDatabaseInfo(PlayersDatabase database)
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

        private void PrintPlayerInfo(PlayerDto player)
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
        private class PlayerDto
        {
            public PlayerDto (string id, string name, int level, bool banned = false)
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
        }

        private class PlayersDatabase
        {
            public const int NotFound = -1;

            private readonly List<PlayerDto> _records = new List<PlayerDto>();

            public void Insert(PlayerDto player)
            {
                if (player == null)
                {
                    throw new ArgumentNullException(nameof(player));
                }

                string guid = Guid.TryParse(player.Id, out Guid parsedGuid) == false
                   ? Guid.NewGuid().ToString("N")
                   : player.Id;

                player.Id = guid;

                int indexToUpate = _records.FindIndex(element => element.Id == player.Id);

                if (indexToUpate == NotFound)
                {
                    _records.Add(player);
                }
                else
                {
                    _records[indexToUpate] = player;
                }
            }

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

                PlayerDto player = new PlayerDto(guid, name, level, banned);

                _records.Add(player);
            }

            public void Ban(string playerId)
            {
                var player = _records.Find(record => record.Id == playerId);

                if (player != null)
                {
                    player.Banned = true;
                }
            }

            public void Unban(string playerId)
            {
                var player = _records.Find(record => record.Id == playerId);

                if (player != null)
                {
                    player.Banned = false;
                }
            }

            public void Delete(string playerId)
            {
                var player = _records.Find(record => record.Id == playerId);

                if (player != null)
                {
                    _records.Remove(player);
                }
            }

            public IReadOnlyCollection<PlayerDto> SelectAllRecords()
            {
                //Представим, что это датасет. Датасет по умолчанию создает копии записей из БД.
                var result = new List<PlayerDto>();

                result = _records
                    .Select(record => CloneRecord(record))
                    .ToList();

                return result;
            }

            private PlayerDto CloneRecord(PlayerDto record)
            {
                if (record == null)
                {
                    throw new ArgumentNullException(nameof(record));
                }
                var result = new PlayerDto(record.Id,record.Name, record.Level, record.Banned);

                return result;
            }
        }
    }
        
}
