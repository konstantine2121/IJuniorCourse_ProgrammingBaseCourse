using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Database;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.DataAccess
{
    public class DAPlayersModule
    {
        private readonly PlayersDatabase _database;

        public DAPlayersModule(PlayersDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            _database = database;
        }

        public bool Ban(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            return Ban(player.Id);
        }

        public bool Ban(string playerId)
        {
            bool result = false;
            var records = _database.SelectAllRecords();
            var player = records.FirstOrDefault(record => record.Id == playerId);

            if (player != null)
            {
                player.Banned = true;
                result = _database.Update(player) != 0;
            }

            return result;
        }

        public bool Unban(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            return Unban(player.Id);
        }

        public bool Unban(string playerId)
        {
            bool result = false;
            var records = _database.SelectAllRecords();
            var player = records.FirstOrDefault(record => record.Id == playerId);

            if (player != null)
            {
                player.Banned = false;
                result = _database.Update(player) != 0;
            }

            return result;
        }

        public bool Add(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }
            
            return _database.Insert(player) != 0;
        }

        public bool Remove(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            return Remove(player.Id);
        }

        public bool Remove(string playerId)
        {
            return _database.Delete(playerId) != 0;
        }

        public List<Player> GetAllRecords()
        {
            return _database.SelectAllRecords().ToList();
        }
    }
}
