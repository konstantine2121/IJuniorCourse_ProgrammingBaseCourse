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
    ///В массивах вы выполняли задание "Динамический массив"<br/>
    ///Используя всё изученное, напишите улучшенную версию динамического массива(не обязательно брать своё старое решение)<br/>
    ///Задание нужно, чтобы вы освоились с List и прощупали его преимущество.<br/>
    ///Проверка на ввод числа обязательна.<br/>
    ///<br/>
    ///Пользователь вводит числа, и программа их запоминает.<br/>
    ///Как только пользователь введёт команду sum, программа выведет сумму всех веденных чисел.<br/>
    ///<br/>
    ///Выход из программы должен происходить только в том случае, если пользователь введет команду exit.<br/>
    /// </summary>
    class SummConsoleInputTask : IRunnable
    {

        #region IRunnable Implementation

        public void Run()
        {            
            const string ExitCommand = "exit";
            const string SumCommand = "sum";

            const string InfoMessage = "Введите число или '"+SumCommand+"' для подсчета. ('"+ExitCommand+"' для выхода.):\n";

            bool exitCalled = false;

            List<int> list = new List<int>();

            while (exitCalled == false)
            {
                var input = ConsoleInputMethods.ReadString(InfoMessage);

                if (int.TryParse(input, out int value))
                {
                    list.Add(value);
                }
                else
                {
                    switch (input)
                    {
                        case SumCommand:                                                            
                                Console.Write("\nИтоговый список:");
                                PrintList(list);
                                Console.WriteLine();

                                var sum = list.Sum();
                                ConsoleOutputMethods.Info("Сумма элементов списка = " + sum);
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

            Console.WriteLine("Завершение работы.");
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void PrintList(List<int> list)
        {   
            int numberInRow = 10;

            for (int i = 0; i < list.Count; i++)
            {
                if (i % numberInRow == 0)
                {
                    Console.WriteLine();
                }

                Console.Write("{0, 4}  ", list[i]);
            }

            Console.WriteLine();
        }
    }
}
