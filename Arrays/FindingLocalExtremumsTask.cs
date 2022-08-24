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
    ///Дан одномерный массив целых чисел из 30 элементов.<br/>
    ///Найдите все локальные максимумы и вывести их. (Элемент является локальным максимумом, если он не имеет соседей, больших, чем он сам)<br/>
    ///<br/>
    ///Крайние элементы являются локальными максимумами если не имеют соседа большего, чем они сами.<br/>
    ///<br/>
    ///Программа должна работать с массивом любого размера.<br/>
    ///<br/>
    ///Массив всех локальных максимумов не нужен.<br/>
    /// </summary>
    class FindingLocalExtremumsTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            int arraySize = 30;

            int[] array = new int[arraySize];

            InitArrayWithRandomValues(array);

            Console.Write("Исходный массив.");
            PrintArray(array);

            Console.WriteLine();
            PrintExtremums(array);

            Console.ReadKey();
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

        private void PrintExtremums(int[] array)
        {
            int minArrayIndex = 0;
            int maxArrayIndex = array.Length-1;

            Console.WriteLine("Перечень экстремумов в массиве:");

            for (int i = 0; i < array.GetLength(0); i++)
            {
                int currentValue = array[i];

                int leftValue = int.MinValue;
                int rightValue = int.MinValue;

                int leftIndex = i - 1;
                int rightIndex = i + 1;

                if (leftIndex >= minArrayIndex)
                {
                    leftValue = array[leftIndex];
                }

                if (rightIndex <= maxArrayIndex)
                {
                    rightValue = array[rightIndex];
                }

                if ( currentValue >= leftValue && currentValue >= rightValue)
                {
                    Console.Write(currentValue + " ");
                }
            }

            Console.WriteLine();
        }

        private void InitArrayWithRandomValues(int[] array)
        {
            int maxExclusiveValue = 101;
            Random random = new Random();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                array[i] = random.Next(maxExclusiveValue);
            }
        }
    }
}
