using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///С помощью Random получить число number, которое не больше 100. Найти сумму всех положительных чисел меньше number (включая число), которые кратные 3 или 5. (К примеру, это числа 3, 5, 6, 9, 10, 12, 15 и т.д.)
    /// </summary>
    class SumOfMultiplicityNumbers : IRunnable
    {
        const int Divider1 = 3;
        const int Divider2 = 5;

        public void Run()
        {
            const int MaxRandomValue = 100;

            Random random = new Random();

            int number = random.Next(MaxRandomValue + 1);

            int summ = 0;

            for (int i = 1; i <= number;i++)
            {
                if (CheckNumberMultiplicity(i))
                {
                    summ += i;
                }
            }

            Console.WriteLine("number = " + number);
            Console.WriteLine("Сумма чисел кратных {0} или {1} =  {2}", Divider1, Divider2, summ);
        }

        private bool CheckNumberMultiplicity(int value)
        {
            if (value % Divider1 == 0 || value % Divider2 == 0)
            {
                return true;
            }
            return false;
        }
    }
}
