using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    /// <summary>
    ///Дано N (1 ≤ N ≤ 27). <br/>
    /// Найти количество трехзначных натуральных чисел, которые кратны N. <br/>
    /// Операции деления (/, %) не использовать. <br/>
    /// А умножение не требуется.<br/>
    ///Число N всего одно, его надо получить в нужном диапазоне. <br/>
    ///<br/>
    /// </summary>
    class CountingNaturalNumbersTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            int number = GetNumberValue();
            int result = CountNaturalNumbers(number);

            Console.WriteLine("Количество трехзначных натуральных чисел, которые кратны N = "+ result);
        }

        #endregion IRunnable Implementation

        private int GetNumberValue()
        {
            int result = -1;

            int minNumberBorder = 1;
            int maxNumberBorder = 27;

            bool inputComplete = false;

            while (inputComplete == false)
            {
                result = ConsoleInputMethods.ReadPositiveInteger("Введите число N: ");

                if (minNumberBorder <= result && result <= maxNumberBorder)
                {
                    inputComplete = true;
                }
                else
                {
                    ConsoleOutputMethods.Warning(string.Format("Число должно попадать в диапазон ({0} <= N <= {1})", minNumberBorder, maxNumberBorder));
                }
            }

            return result;
        }

        private int CountNaturalNumbers(int n)
        {
            int result = 0;

            int minRange = 100;
            int maxRange = 1000-1;

            for (int i = n; i < maxRange; i+=n)
            {
                if (minRange <= i)
                {
                    result++;
                }
            }

            return result;
        }
    }
}
