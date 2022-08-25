using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
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
    ///В массиве чисел найдите самый длинный подмассив из одинаковых чисел.<br/>
    ///Дано 30 чисел. Вывести в консоль сам массив, число, которое само больше раз повторяется подряд и количество повторений.<br/>
    ///Дополнительный массив не надо создавать.<br/>
    ///Пример: {5, 5, 9, 9, 9, 5, 5} - число 9 повторяется большее число раз подряд.<br/>
    /// </summary>
    class FindingTheLongestSubarrayTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            int arrayLength = 30;
            
            int[] array = new int[arrayLength];
            
            InitArrayWithRandomValues(array);

            PrintArray(array);

            int maxSubbarrayNumber = -1;
            int maxSubbarrayLength = 0;

            int currentSubarrayLengthCounter = 1;

            for (int i = 1; i < array.Length; i++)
            {   
                if (array[i] == array[i - 1])
                {
                   currentSubarrayLengthCounter++;
                }
                else
                {
                    currentSubarrayLengthCounter = 1;
                }

                if (currentSubarrayLengthCounter > maxSubbarrayLength)
                {
                    maxSubbarrayLength = currentSubarrayLengthCounter;
                    maxSubbarrayNumber = array[i];
                }
            }

            Console.WriteLine($"\nЧисло {maxSubbarrayNumber} повторяется большее число раз подряд. Количество повторений {maxSubbarrayLength}");
        }

        #endregion IRunnable Implementation

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

        private void InitArrayWithRandomValues(int[] array)
        {
            int maxExclusiveValue = 10;
            Random random = new Random();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                array[i] = random.Next(maxExclusiveValue);
            }
        }
    }
}
