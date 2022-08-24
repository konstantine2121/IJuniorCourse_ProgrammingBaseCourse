using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    /// <summary>
    ///При помощи всего, что вы изучили, создать приложение, которое может обрабатывать команды. Т.е. вы создаете меню, ожидаете ввода нужной команды, после чего выполняете действие, которое присвоено этой команде.<br/>
    ///<br/>
    ///Примеры команд (требуется 4-6 команд, придумать самим):<br/>
    ///<br/>
    ///SetName – установить имя;<br/>
    ///<br/>
    ///ChangeConsoleColor- изменить цвет консоли;<br/>
    ///<br/>
    ///SetPassword – установить пароль;<br/>
    ///<br/>
    ///WriteName – вывести имя (после ввода пароля);<br/>
    ///<br/>
    ///Esc – выход из программы.<br/>
    ///<br/>
    ///Программа не должна завершаться после ввода, пользователь сам должен выйти из программы при помощи команды. 
    /// </summary>
    class ConsoleCommanderTask : IRunnable
    {
        private const string ExitCommand = "q";
        private const string SetNameCommand = "n";
        private const string ChangeConsoleColorCommand = "color";
        private const string SetPasswordCommand = "pwd";
        private const string WriteNameCommand = "who";
        private const string HelpCommand = "h";
        private const string ShowInfoCommand = "info";
        
        private const string HelpText = @"
q - выход из консоли

n - ввести имя пользователя

color - установить цвет консоли

pwd - ввести пароль

who - активные пользователи

info - вывести приватную информацию

h - вывод текущей справки
";

        private string _name = "Bob";
        private string _password = string.Empty;

        private enum CommandType
        {
            EmptyCommand = -2,
            InvalidCommand = -1,
            Exit,
            SetName,
            ChangeConsoleColor,
            SetPassword,
            WriteName,
            ShowInfo,
            Help
        }

        #region IRunnable Implementation

        public void Run()
        {
            InitStartValues();
            PrintHelp();

            InitializePassword();

            bool exitSignal = false;

            while (exitSignal == false)
            {
                PrintShellBegin();

                string input = Console.ReadLine();

                CommandType commandType = GetCommandType(input);

                if (commandType == CommandType.EmptyCommand)
                {
                    continue;
                }

                switch (commandType)
                {
                    case CommandType.Exit:
                        exitSignal = true;
                        break;
                    case CommandType.SetName:
                        RunSetNameCommand();
                        break;
                    case CommandType.ChangeConsoleColor:
                        RunChangeConsoleColorCommand();
                        break;
                    case CommandType.SetPassword:
                        RunSetPasswordCommand();
                        break;
                    case CommandType.WriteName:
                        RunWriteNameCommand();
                        break;
                    case CommandType.ShowInfo:
                        RunShowInfoCommand();
                        break;
                    case CommandType.Help:
                        PrintHelp();
                        break;

                    default:
                        Console.WriteLine("Команда не найдена.");
                        break;
                }
            }

            Console.WriteLine("Выход из программы.\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private bool PasswordSetted
        {
            get { return string.IsNullOrEmpty(_password) == false; }
        }

        private void InitializePassword()
        {
            if (PasswordSetted)
            {
                ConsoleOutputMethods.Info("Наличие пароля в системе подтверждено.");
            }
            else
            {
                ConsoleOutputMethods.Warning("Пароль не найден. Необходимо его задать.");
                _password = ConsoleInputMethods.ReadString("Введите пароль:");
                ConsoleOutputMethods.Info("Пароль успешно сменен.");
            }
        }

        private void InitStartValues()
        {            
            _name = "Bob";
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void PrintHelp()
        {
            ConsoleOutputMethods.Info("Список доступных комманд");
            Console.WriteLine(HelpText);
        }

        private void PrintShellBegin()
        {
            Console.Write("~$ ");
        }

        private CommandType GetCommandType(string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                return CommandType.EmptyCommand;
            }

            switch (commandText)
            {
                case ExitCommand:
                    return CommandType.Exit;

                case SetNameCommand:
                    return CommandType.SetName;

                case ChangeConsoleColorCommand:
                    return CommandType.ChangeConsoleColor;

                case SetPasswordCommand:
                    return CommandType.SetPassword;

                case WriteNameCommand:
                    return CommandType.WriteName;

                case ShowInfoCommand:
                    return CommandType.ShowInfo;

                case HelpCommand:
                    return CommandType.Help;
            };

            return CommandType.InvalidCommand;
        }

        private void RunSetNameCommand()
        {
            var emptyValue = true;

            while (emptyValue == true)
            {
                Console.WriteLine("Введите имя:");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) == false)
                {
                    emptyValue = false;
                    _name = input;
                }
                else
                {
                    ConsoleOutputMethods.Warning("Имя не может быть пустым!");
                }
            }
        }

        private void RunChangeConsoleColorCommand()
        {
            ConsoleOutputMethods.Info("Выберете цвет из списка предложенных.");
            PrintColorsInfo();

            var input = ConsoleInputMethods.ReadPositiveInteger("Введите цвет: ");

            if (GetAllowedConsoleColors().Contains((ConsoleColor)input))
            {
                Console.ForegroundColor = (ConsoleColor)input;
            }
            else
            {
                ConsoleOutputMethods.Warning("Указан неверный цвет!");
            }
        }

        private void PrintColorsInfo()
        {
            var foregroundColor = Console.ForegroundColor;

            var colors = GetAllowedConsoleColors();

            var colorValuesInRow = 4;

            Console.WriteLine();

            for (int i = 0; i < colors.Count; i += colorValuesInRow)
            {
                for (int j = i; j < Math.Min(colors.Count, i + colorValuesInRow); j++)
                {
                    var color = colors[j];
                    var value = string.Format("{0,-12}{1,4}\t", color, (int)color);
                    
                    Console.ForegroundColor = color;
                    Console.Write(value);
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = foregroundColor;
        }

        private List<ConsoleColor> GetAllowedConsoleColors()
        {
            return Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>()
                .Where(color => color != ConsoleColor.Black).ToList();
        }

        private void RunSetPasswordCommand()
        {
            var input = ConsoleInputMethods.ReadString("Введите старый пароль:");

            if (input.Equals(_password))
            {
                _password = ConsoleInputMethods.ReadString("Введите новый пароль:");                
                ConsoleOutputMethods.Info("Пароль успешно сменен.");
            }
            else
            {
                ConsoleOutputMethods.Warning("Указан неверный пароль!");
            }
        }

        private void RunWriteNameCommand()
        {
            Console.WriteLine("Имя пользователя: " + _name);
        }
        
        private void RunShowInfoCommand()
        {
            var input = ConsoleInputMethods.ReadString("Введите пароль:");

            if (input.Equals(_password))
            {                
                ConsoleOutputMethods.Info("Здесь могла быть Ваша реклама!");
            }
            else
            {
                ConsoleOutputMethods.Warning("Доступ запрещен.");
            }
        }
    }
}
