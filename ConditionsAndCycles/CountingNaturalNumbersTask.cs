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
            int n = GetNValue();
            int result = CountNaturalNumbers(n);

            Console.WriteLine("Количество трехзначных натуральных чисел, которые кратны N = "+ result);
        }

        #endregion IRunnable Implementation

        private int GetNValue()
        {
            int result = -1;

            int min = 1;
            int max = 27;

            bool inputComplete = false;

            while (inputComplete == false)
            {
                result = ConsoleInputMethods.ReadPositiveInteger("Введите число N: ");
                if (min <= result && result <= max)
                {
                    inputComplete = true;
                }
                else
                {
                    ConsoleOutputMethods.Warning(string.Format("Число должно попадать в диапазон ({0} <= N <= {1})", min, max));
                }
            }

            return result;
        }

        private int CountNaturalNumbers(int n)
        {
            int result = 0;

            int min = 100;
            int max = 1000-1;

            for (int i = n; i < max; i+=n)
            {
                if (min <= i && i <= max)
                {
                    result++;
                }
            }

            return result;
        }
    }
}
