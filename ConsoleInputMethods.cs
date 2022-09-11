using System;

namespace IJuniorCourse_ProgrammingBaseCourse
{
    /// <summary>
    /// Методы для считывания значений с консоли.
    /// </summary>
    public static class ConsoleInputMethods
    {
        /// <summary>
        /// Получить ответ на вопрос.
        /// </summary>
        /// <param name="question">Вопрос.</param>
        /// <returns>Yes/No</returns>
        public static DialogResult GetDialogResult(string question)
        {
            var infoCommands = DialogResult.Yes + " - " + (int)DialogResult.Yes + ", " + DialogResult.No + " - " + (int)DialogResult.No;
            var result = DialogResult.No;

            var correct = false;

            while (correct == false)
            {
                var input = (DialogResult)ConsoleInputMethods.ReadPositiveInteger(question + $"({infoCommands}): ");
                switch (input)
                {
                    case DialogResult.Yes:
                    case DialogResult.No:
                        result = input;
                        correct = true;
                        break;

                    default:
                        ConsoleOutputMethods.Warning("Такой опции нет в списке!");
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Считывает с консоли положительное значение натурального числа.
        /// </summary>
        /// <param name="message">Информационное сообщение, отображаемое перед вводом данных.</param>
        /// <returns>Положительное значение числа.</returns>
        public static int ReadPositiveInteger(string message)
        {
            int result = 0;
            bool parsed = false;

            while (parsed == false)
            {
                Console.Write(message);
                var input = Console.ReadLine();
                parsed = int.TryParse(input, out result);

                if (parsed == false)
                {
                    ConsoleOutputMethods.Warning("Не получилось распознать значение. Попробуйте еще раз.");
                }
                else
                {
                    if (result < 0)
                    {
                        parsed = false;
                        ConsoleOutputMethods.Warning("Значение не может быть отрицательным. Попробуйте еще раз.");                        
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Считывает с консоли значение натурального числа.
        /// </summary>
        /// <param name="message">Информационное сообщение, отображаемое перед вводом данных.</param>
        /// <returns>Значение числа.</returns>
        public static int ReadInteger(string message)
        {
            int result = 0;
            bool parsed = false;

            while (parsed == false)
            {
                Console.Write(message);
                var input = Console.ReadLine();
                parsed = int.TryParse(input, out result);

                if (parsed == false)
                {
                    ConsoleOutputMethods.Warning("Не получилось распознать значение. Попробуйте еще раз.");
                }
            }

            return result;
        }

        /// <summary>
        /// Считывает с консоли значение строки.
        /// </summary>
        /// <param name="message">Информационное сообщение, отображаемое перед вводом данных.</param>
        /// <returns></returns>
        public static string ReadString(string message)
        {
            string input = string.Empty;

            while (string.IsNullOrEmpty(input))
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    ConsoleOutputMethods.Warning("Введена пустая строка. Попробуйте еще раз.");
                }
            }

            return input;
        }
    }
}

