using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Interfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Views
{
    class PlayersModuleView : IPlayersModuleView
    {
        private ConsoleRecord _totalRecords;
        private ConsoleRecord _listOfBanned;
        private ConsoleRecord _databaseContent;
        private ConsoleRecord _inputField;
        private ConsoleRecord _commandResult;
        private ConsoleRecord _commandsList;

        public PlayersModuleView()
        {
            Inititialize();
        }

        public ConsoleRecord TotalRecords { get { return _totalRecords; }  }

        public ConsoleRecord ListOfBanned { get { return _listOfBanned; } }

        public ConsoleRecord DatabaseContent { get { return _databaseContent; } }

        public ConsoleRecord InputField { get { return _inputField; } }

        public ConsoleRecord CommandResult { get { return _commandResult; } }

        public ConsoleRecord CommandsList{ get { return _commandsList; } }

        public void Update()
        {
            Console.Clear();

            TotalRecords.Update();
            ListOfBanned.Update();
            DatabaseContent.Update();
            CommandResult.Update();
            CommandsList.Update();
            InputField.Update();
        }

        private void Inititialize()
        {
            _commandsList = new ConsoleRecord(0, 0);//3 строки
            _listOfBanned = new ConsoleRecord(80, 0);

            _commandResult = new ConsoleRecord(0, 5);
            _commandResult.ForegroundColor = ConsoleColor.Yellow;

            _inputField = new ConsoleRecord(0, 6);

            _totalRecords = new ConsoleRecord(0, 10);
            _databaseContent = new ConsoleRecord(0, 11);
        }
    }
}
