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
        private enum CommandType
        {
            EmptyCommand = -2,
            InvalidCommand = -1,
            Exit,
            SetName,
            ChangeConsoleColor,
            SetPassword,
            WriteName,
            Help
        }

        const string ExitCommand = "q";
        const string SetNameCommand = "n";
        const string ChangeConsoleColorCommand = "color";
        const string SetPasswordCommand = "pwd";
        const string WriteNameCommand = "who";
        const string HelpCommand = "h";

        const string HelpText = @"
q - выход из консоли

n - ввести имя пользователя

color - установить цвет консоли

pwd - ввести пароль

who - активные пользователи

h - вывод текущей справки
";

        private bool _exitSignal = false;

        private string _name = "Bob";
        private string _password = string.Empty;
        private ConsoleColor _consoleColor = ConsoleColor.White;

        public void Run()
        {
            InitStartValues();
            PrintHelp();

            while (_exitSignal == false)
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
                        _exitSignal = true;
                        break;
                    case CommandType.SetName:

                        break;
                    case CommandType.ChangeConsoleColor:

                        break;
                    case CommandType.SetPassword:

                        break;
                    case CommandType.WriteName:

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

        private void InitStartValues()
        {
            _exitSignal = false;
            _name = "Bob";
            _password = string.Empty;
            _consoleColor = ConsoleColor.White;
        }

        private void PrintShellBegin()
        {
            Console.Write("~$ ");
        }

        private void PrintHelp()
        {
            ConsoleOutputMethods.Info("Список доступных комманд");
            Console.WriteLine(HelpText);
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

                case HelpCommand:
                    return CommandType.Help;
            };

            return CommandType.InvalidCommand;
        }


    }
}
