using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.Arrays
{
    /// <summary>
    ///Дан массив чисел (минимум 10 чисел). Надо вывести в консоль числа отсортированы, от меньшего до большего.<br/>
    ///Нельзя использовать Array.Sort. Можно найти подходящий алгоритм сортировки и использовать его для задачи. 
    /// </summary>
    class BubbleSortingTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            int arrayLength = 30;
            int[] array = new int[arrayLength];

            InitArrayWithRandomValues(array);
            ConsoleOutputMethods.Info("Исходный массив.");
            PrintArray(array);
            PerformBubbleSorting(array);
            ConsoleOutputMethods.Info("Отсортированный массив.");
            PrintArray(array);
        }

        #endregion IRunnable Implementation

        private void PerformBubbleSorting(int[] array)
        {
            int temp = 0;
            int lastIndex = array.Length - 1;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < lastIndex - i; j++)
                {
                    if (array[j] > array[j+1])
                    {
                        temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        private void PrintArray(int[] array)
        {
            Console.WriteLine();

            int numberInRow = 10;

            for (int i = 0; i < array.Length; i++)
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

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(maxExclusiveValue);
            }
        }
    }
}
