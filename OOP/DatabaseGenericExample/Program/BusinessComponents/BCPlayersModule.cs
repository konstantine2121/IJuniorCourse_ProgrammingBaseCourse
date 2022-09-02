using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.DataAccess;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Dto;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.BusinessComponents
{
    public class BCPlayersModule
    {
        public event Action ModelChanged;

        private readonly DAPlayersModule _daPlayersDatabase;

        public BCPlayersModule(DAPlayersModule daPlayersDatabase)
        {
            if (daPlayersDatabase == null)
            {
                throw new ArgumentNullException(nameof(daPlayersDatabase));
            }

            _daPlayersDatabase = daPlayersDatabase;
        }

        public bool Ban(Player player)
        {
            bool result = _daPlayersDatabase.Ban(player.Id);
            
            if (result)
            {
                ModelChanged?.Invoke();
            }
            return result;
        }

        public bool Ban(string playerId)
        {
            bool result = _daPlayersDatabase.Ban(playerId);

            if (result)
            {
                ModelChanged?.Invoke();
            }
            return result;
        }

        public bool Unban(Player player)
        {
            bool result = _daPlayersDatabase.Unban(player.Id);

            if (result)
            {
                ModelChanged?.Invoke();
            }
            return result;
        }

        public bool Unban(string playerId)
        {
            bool result = _daPlayersDatabase.Unban(playerId);

            if (result)
            {
                ModelChanged?.Invoke();
            }
            return result;
        }

        public bool Add(Player player)
        {
            bool result = _daPlayersDatabase.Add(player);

            if (result)
            {
                ModelChanged?.Invoke();
            }
            return result;
        }

        public bool Remove(Player player)
        {
            bool result = _daPlayersDatabase.Remove(player);

            if (result)
            {
                ModelChanged?.Invoke();
            }
            return result;
        }

        public bool Remove(string playerId)
        {
            bool result = _daPlayersDatabase.Remove(playerId);

            if (result)
            {
                ModelChanged?.Invoke();
            }
            return result;
        }

        public List<Player> GetAllRecords()
        {
            return _daPlayersDatabase.GetAllRecords();
        }
    }
}
