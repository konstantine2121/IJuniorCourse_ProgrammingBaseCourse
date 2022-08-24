﻿using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.Arrays
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Пользователь вводит числа, и программа их запоминает.<br/>
    ///Как только пользователь введёт команду sum, программа выведет сумму всех веденных чисел.<br/>
    ///<br/>
    ///Выход из программы должен происходить только в том случае, если пользователь введет команду exit.<br/>
    ///Если введено не sum и не exit, значит это число и его надо добавить в массив.<br/>
    ///<br/>
    ///Программа должна работать на основе расширения массива.<br/>
    ///<br/>
    ///Внимание, нельзя использовать List<T> и Array.Resize<br/>
    /// </summary>
    class SummConsoleInputTask : IRunnable
    {

        #region IRunnable Implementation

        public void Run()
        {            
            const string ExitCommand = "exit";
            const string SummCommand = "summ";

            const string InfoMessage = "Введите число или '"+SummCommand+"' для подсчета. ('"+ExitCommand+"' для выхода.):\n";

            bool exitCalled = false;
            bool summCalled = false;

            int arrayStartLength = 0;

            int[] array = new int[arrayStartLength];

            while (exitCalled == false && summCalled == false)
            {
                var input = ConsoleInputMethods.ReadString(InfoMessage);

                int value = -1;
                if (int.TryParse(input, out value))
                {
                    array = ResizeArray(array);
                    array[array.Length - 1] = value;                    
                }
                else
                {
                    switch (input)
                    {
                        case SummCommand:
                            summCalled = true;
                            break;

                        case ExitCommand:
                            exitCalled = true;
                            break;

                        default:
                            ConsoleOutputMethods.Warning("Команда не найдена.");
                            break;

                    }
                }
            }

            if (exitCalled)
            {
                ConsoleOutputMethods.Warning("Аварийное завершение работы.");
                Console.ReadKey();
                return;
            }

            if (summCalled)
            {
                Console.WriteLine();
                
                Console.Write("Итоговый массив:");
                PrintArray(array);
                
                Console.WriteLine();

                var summ = GetArraySumm(array);
                ConsoleOutputMethods.Info("Сумма элементов массива = "+ summ);
            }

            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private int [] ResizeArray(int [] array)
        {
            int[] result = new int[array.Length + 1];

            for (int i=0;i< array.Length;i++)
            {
                result[i] = array[i];
            }

            return result;
        }

        private int GetArraySumm(int[] array)
        {
            int result = 0;

            for (int i = 0; i < array.Length; i++)
            {
                result += array[i];
            }

            return result;
        }

        private void PrintArray(int[] array)
        {
            Console.WriteLine();

            int numberInRow = 10;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (i % numberInRow == 0)
                {
                    Console.WriteLine();
                }

                Console.Write("{0, 4}  ", array[i]);
            }

            Console.WriteLine();
        }
    }
}
