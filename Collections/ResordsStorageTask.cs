using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.Collections
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///В функциях вы выполняли задание "Кадровый учёт"<br/>
    ///Используя одну из изученных коллекций, вы смогли бы сильно себе упростить код выполненной программы, ведь у нас данные, это ФИО и позиция.<br/>
    ///Поиск в данном задании не нужен.<br/>
    ///<br/>
    ///1) добавить досье<br/>
    ///<br/>
    ///2) вывести все досье (в одну строку через “-” фио и должность)<br/>
    ///<br/>
    ///3) удалить досье<br/>
    ///<br/>
    ///4) выход
    /// </summary>
    class ResordsStorageTask : IRunnable
    {
        private string PrintTemplate = "{0}  -  {1}";

        private Dictionary<string, string> _records;

        public enum CommandType
        {
            Add = 1,
            Print,
            Remove,
            Exit
        }

        #region IRunnable Implementation

        public void Run()
        {
            string commandsInfo = GetCommandsInfo();

            _records = new Dictionary<string, string>();

            bool exitCalled = false;

            while(exitCalled == false)
            {
                int command = ConsoleInputMethods.ReadPositiveInteger(commandsInfo+"\nВведите № команды: ");

                switch ((CommandType)command)
                {
                    case CommandType.Add:
                        RunAddCommand();
                        break;

                    case CommandType.Print:
                        RunPrintCommand();
                        break;

                    case CommandType.Remove:
                        RunRemoveCommand();
                        break;

                    case CommandType.Exit:
                        exitCalled = true;
                        break;

                    default:
                        ConsoleOutputMethods.Warning(string.Format("Указанной команды '{0}' нет в списке доступных.", command));
                        break;
                }
            }

            Console.WriteLine("Выход из программы.");
            Console.ReadKey();

        }

        #endregion IRunnable Implementation

        private string GetCommandsInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string separator = ", ";

            var commandTypes = GetCommandTypes();

            stringBuilder.Append("Доступные комманды: ");

            foreach (var type in commandTypes)
            {
                stringBuilder.AppendFormat("{0} - {1}{2}",type, (int)type,separator);
            }

            if (stringBuilder.Length > separator.Length && commandTypes.Count() > 0)
            {
                stringBuilder.Remove(stringBuilder.Length - separator.Length, separator.Length);
            }

            return stringBuilder.ToString(); ;
        }

        private IEnumerable<CommandType> GetCommandTypes()
        {
            return Enum.GetValues(typeof(CommandType)).Cast<CommandType>();
        }

        #region Commands

        private void RunAddCommand() 
        {
            var name = ConsoleInputMethods.ReadString("Введите имя: ");
            var post = ConsoleInputMethods.ReadString("Введите должность: ");

            name = name.ToLower();

            if (_records.ContainsKey(name) == false)
            {
                _records.Add(name, post);
            }
            else
            {
                ConsoleOutputMethods.Warning("Такая запись уже существует.");
            }

            Console.WriteLine();
        }

        private void RunPrintCommand()
        {
            ConsoleOutputMethods.Info("Вывод списка работников:");

            if (_records.Keys.Count == 0)
            {
                ConsoleOutputMethods.Warning("Список пуст.\n");
                return;
            }

            foreach (var pair in _records)
            {
                Console.WriteLine(PrintTemplate, pair.Key, pair.Value);
            }

            Console.WriteLine();
        }

        private void RunRemoveCommand() 
        {            
            var  nameToDelete = ConsoleInputMethods.ReadString("Введите ФИО удаляемого элемента: ");

            nameToDelete = nameToDelete.ToLower();

            if (_records.Keys.Contains(nameToDelete))
            {
                _records.Remove(nameToDelete);
            }
            else
            {
                ConsoleOutputMethods.Warning("Данная запись не найдена.");
            }

            Console.WriteLine();
        }

        #endregion Commands
    }
}
