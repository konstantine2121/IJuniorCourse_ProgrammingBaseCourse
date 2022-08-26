using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.Functions
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Будет 2 массива: 1) фио 2) должность.<br/>
    ///<br/>
    ///Описать функцию заполнения массивов досье, функцию форматированного вывода, функцию поиска по фамилии и функцию удаления досье.<br/>
    ///<br/>
    ///Функция расширяет уже имеющийся массив на 1 и дописывает туда новое значение.<br/>
    ///<br/>
    ///Программа должна быть с меню, которое содержит пункты:<br/>
    ///<br/>
    ///1) добавить досье<br/>
    ///<br/>
    ///2) вывести все досье (в одну строку через “-” фио и должность с порядковым номером в начале)<br/>
    ///<br/>
    ///3) удалить досье (Массивы уменьшаются на один элемент. Нужны дополнительные проверки, чтобы не возникало ошибок)<br/>
    ///<br/>
    ///4) поиск по фамилии<br/>
    ///<br/>
    ///5) выход<br/>
    /// </summary>
    class ResordsStorageTask : IRunnable
    {
        private string PrintTemplate = "{0,3}  {1}  -  {2}";

        private string[] _names;
        private string[] _posts;

        public enum CommandType
        {
            Add = 1,
            Print,
            Remove,
            Find,
            Exit
        }

        #region IRunnable Implementation

        public void Run()
        {
            string commandsInfo = GetCommandsInfo();

            _names = new string[0];
            _posts = new string[0];

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

                    case CommandType.Find:
                        RunFindCommand();
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

            _names = EnlargeArray(_names);
            _posts = EnlargeArray(_posts);

            _names[_names.Length - 1] = name;
            _posts[_posts.Length - 1] = post;

            Console.WriteLine();
        }

        private void RunPrintCommand()
        {
            ConsoleOutputMethods.Info("Вывод списка работников:");

            if (_names.Length == 0 || _posts.Length ==0)
            {
                ConsoleOutputMethods.Warning("Список пуст.\n");
                return;
            }

            for (int i = 0; i < _names.Length && i < _posts.Length; i++)
            {
                Console.WriteLine(PrintTemplate, i, _names[i], _posts[i]);
            }

            Console.WriteLine();
        }

        private void RunRemoveCommand() 
        {
            int arrayLength = Math.Min(_names.Length, _posts.Length);
            int lastArrayIndex = arrayLength - 1;

            int indexToRemove = ConsoleInputMethods.ReadPositiveInteger("Введите индекс удаляемого элемента: ");

            if (indexToRemove >= arrayLength)
            {
                ConsoleOutputMethods.Warning($"Значение индекса({indexToRemove}) превышает индекс последнего элемента массива({lastArrayIndex})");
            }
            else
            {
                _names = RemoveElement(_names, indexToRemove);
                _posts = RemoveElement(_posts, indexToRemove);
            }

            Console.WriteLine();
        }

        private void RunFindCommand() 
        {
            int arrayLength = Math.Min(_names.Length, _posts.Length);

            if (arrayLength == 0)
            {
                ConsoleOutputMethods.Warning("Поиск невозможен. Список пуст.\n");
                return;
            }

            string name = ConsoleInputMethods.ReadString("Введите имя для поиска: ");
            
            for(int i=0; i< arrayLength;i++)
            {
                if (_names[i].ToLower().Contains(name.ToLower()))
                {
                    Console.WriteLine(PrintTemplate, i, _names[i], _posts[i]);
                }
            }

            Console.WriteLine();
        }

        #endregion Commands

        #region Arrays Functions

        private string[] EnlargeArray(string[] array)
        {
            string[] result = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i];
            }

            return result;
        }

        private string[] RemoveElement(string[] array, int indexToRemove)
        {
            if (indexToRemove >= array.Length)
            {
                throw new IndexOutOfRangeException();
            }

            string[] result = new string[array.Length -1];

            for (int i = 0,j =0; i < array.Length && j < result.Length; i++, j++)
            {
                if (i == indexToRemove)
                {
                    i++;
                }
                result[j] = array[i];
            }

            return result;
        }

        #endregion Arrays Functions
    }
}
