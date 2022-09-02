using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.BusinessComponents;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Dto;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Controllers
{
    public class PlayersModuleController
    {
        private const string ExitCommand = "q";
        private const string AddCommand = "add";
        private const string RemoveCommand = "rm";
        private const string BanCommand = "ban";
        private const string UnbanCommand = "unban";

        //по хорошему тут нужно Loader запихивать. Как доп. прослойку меж BC и контроллером.
        private BCPlayersModule _bcPlayersDatabase;
        private IPlayersModuleView _view;

        public PlayersModuleController(BCPlayersModule bcPlayersDatabase, IPlayersModuleView view)
        {
            if (bcPlayersDatabase == null)
            {
                throw new ArgumentNullException(nameof(bcPlayersDatabase));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            _bcPlayersDatabase = bcPlayersDatabase;
            _view = view;

            // В консольном приложении это не очень удобно. Лучше дергать руками.
            //
            _bcPlayersDatabase.ModelChanged += () => { 
                UpdateViewInfo();
            };
        }

        public void RunMainCycle()
        {
            bool exitCalled = false;

            UpdateViewInfo();
            _view.Update();

            while (exitCalled ==false)
            {
                var input = Console.ReadLine();

                switch (input)
                {
                    case ExitCommand:
                        exitCalled = true;
                        _view.CommandResult.Text = "Выход из программы";
                        break;

                    case AddCommand:
                        RunAddCommand();
                        break;
                    case RemoveCommand:
                        RunRemoveCommand();
                        break;
                    case BanCommand:
                        RunBanCommand();
                        break;
                    case UnbanCommand:
                        RunUnbanCommand();
                        break;

                    default:
                        _view.CommandResult.Text = "Команда не найдена.";
                        break;
                }

                _view.Update();
            }
        }

        #region Commands

        private void RunAddCommand()
        {
            Console.Clear();
            ConsoleOutputMethods.Info("Добавление игрока.");

            string name = ConsoleInputMethods.ReadString("Введите имя игрока: ");
            int level = ConsoleInputMethods.ReadPositiveInteger("Введите уровень игрока: ");

            Player player = new Player(string.Empty,name,level);

            if (_bcPlayersDatabase.Add(player))
            {
                _view.CommandResult.Text = "Запись успешно добавлена.";
            }
            else
            {
                _view.CommandResult.Text = "Ошибка при добавлении.";
            }

        }

        private void RunRemoveCommand()
        {
            Console.Write("Введите id игрока: ");
            string id = Console.ReadLine();

            if (_bcPlayersDatabase.Remove(id))
            {
                _view.CommandResult.Text = "Запись успешно удалена.";
            }
            else
            {
                _view.CommandResult.Text = "Ошибка при удалении.";
            }
        }

        private void RunBanCommand()
        {
            Console.Write("Введите id игрока для бана: ");
            string id = Console.ReadLine();

            if (_bcPlayersDatabase.Ban(id))
            {
                _view.CommandResult.Text = "Игрок забанен.";
            }
            else
            {
                _view.CommandResult.Text = "Игрок не найден.";
            }
        }

        private void RunUnbanCommand()
        {
            Console.Write("Введите id игрока для разбана: ");
            string id = Console.ReadLine();

            if (_bcPlayersDatabase.Unban(id))
            {
                _view.CommandResult.Text = "Игрок разбанен.";
            }
            else
            {
                _view.CommandResult.Text = "Игрок не найден.";
            }
        }

        #endregion Commands


        #region Update View

        private void UpdateViewInfo()
        {
            _view.ListOfBanned.Text = GetBannedInfo();
            _view.InputField.Text = "Введите команду: ";
            _view.CommandsList.Text = GetCommandsInfo();
            _view.TotalRecords.Text = "Всего записей: " + _bcPlayersDatabase.GetAllRecords().Count;
            _view.DatabaseContent.Text = GetDatabaseContent();
        }

        private string GetBannedInfo()
        {
            var banned = _bcPlayersDatabase.GetAllRecords()
                .Where(record => record.Banned);

            StringBuilder builder = new StringBuilder();

            builder.Append("Всего забанено: ");
            builder.AppendLine(banned.Count().ToString());

            foreach(var player in banned)
            {
                builder.AppendLine(player.Name);
            }

            return builder.ToString();
        }

        private string GetCommandsInfo()
        {   
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Список доступных комманд: "+ ExitCommand +" - выход из программы;");
            builder.AppendLine(AddCommand + " - добавить; "+RemoveCommand+ " - удалить;");
            builder.AppendLine(BanCommand + " - забанить; "+ UnbanCommand + " - помиловать.");
         
            return builder.ToString();
        }

        private string GetDatabaseContent()
        {
            const string playerRecordPrintFormat = "{0, 32} {1, 10} {2, 4} {3}";

            StringBuilder builder = new StringBuilder();

            var records = _bcPlayersDatabase.GetAllRecords();

            builder.AppendLine(string.Format(playerRecordPrintFormat, "Id", "Имя", "lvl", "Забанен"));

            foreach (var record in records)
            {
                var line = string.Format(playerRecordPrintFormat, record.Id, record.Name, record.Level, record.Banned);
                builder.AppendLine(line);
            }

            return builder.ToString();
        }

        #endregion Update View
    }
}
