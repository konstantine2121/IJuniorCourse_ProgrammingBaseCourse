using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Найдите минимальную степень двойки, превосходящую заданное число.<br/>
    ///К примеру, для числа 4 будет 2 в степени 3, то есть 8. 4	&lt; 8.<br/>
    ///Для числа 29 будет 2 в степени 5, то есть 32. 29 &lt; 32.<br/>
    ///В консоль вывести число (лучше получить от Random), степень и само число 2 в найденной степени.
    /// </summary>
    class ExponentCounterTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            const int TwoToPowerOfZero = 1;
            const int MaxRandomValue = 1000;

            Random random = new Random();

            int powerCounter = 0;
            int currentValue = TwoToPowerOfZero;
            int shiftValue = 1;
            int randomValue = random.Next(MaxRandomValue);

            while (currentValue <= randomValue)
            {
                currentValue <<= shiftValue;
                powerCounter++;
            }

            Console.WriteLine("Случайное число: "+ randomValue);

            Console.WriteLine("2 ^ {0} = {1}\n{1} > {2}",powerCounter,currentValue,randomValue);
        }

        #endregion IRunnable Implementation
    }
}
