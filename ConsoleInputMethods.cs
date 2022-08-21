﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse
{
    /// <summary>
    /// Методы для считывания значений с консоли.
    /// </summary>
    public static class ConsoleInputMethods
    {
        /// <summary>
        /// Считывает с консоли положительное значение натурального числа.
        /// </summary>
        /// <param name="message">Информационное сообщение, отображаемое перед вводом данных.</param>
        /// <returns>Положительное значение числа.</returns>
        public static int ReadPosistiveInteger(string message)
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
                    Console.WriteLine("Не получилось распознать значение. Попробуйте еще раз.");
                }
                else
                {
                    if (result < 0)
                    {
                        Console.WriteLine("Значение не может быть отрицательным. Попробуйте еще раз.");
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
                    Console.WriteLine("Не получилось распознать значение. Попробуйте еще раз.");
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
                    Console.WriteLine("Введена пустая строка. Попробуйте еще раз.");
                }
            }

            return input;
        }
    }
}

