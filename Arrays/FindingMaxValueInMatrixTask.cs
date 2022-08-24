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
    ///Найти наибольший элемент матрицы A(10,10) и записать ноль в те ячейки, где он находятся. Вывести наибольший элемент, исходную и полученную матрицу.<br/>
    ///Массив под измененную версию не нужен.<br/>
    /// </summary>
    class FindingMaxValueInMatrixTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            int arraySize = 10;

            int[,] array = new int[arraySize, arraySize];

            InitArrayWithRandomValues(array);

            Console.WriteLine("Исходная матрица.");
            PrintArray(array);
            Console.WriteLine();

            int maxValue = GetMaxArrayValue(array);
            Console.WriteLine("Максимальное значение в массиве = " + maxValue);
            Console.WriteLine();
            ReplaceMaxArrayValues(array, maxValue);

            Console.WriteLine("Зануленная матрица.");
            PrintArray(array);
        }

        #endregion IRunnable Implementation

        private void InitArrayWithRandomValues(int[,] array)
        {
            int maxExclusiveValue = 101;
            Random random = new Random();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(maxExclusiveValue);
                }
            }
        }

        private void PrintArray(int[,] array)
        {
            Console.WriteLine();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write("{0, 5}\t", array[i, j]);
                }
                Console.WriteLine();
            }
        }

        private int GetMaxArrayValue(int[,] array)
        {
            int max = int.MinValue;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] > max)
                    {
                        max = array[i, j];
                    }
                }
            }

            return max;
        }

        private void ReplaceMaxArrayValues(int[,] array, int max)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == max)
                    {
                        array[i, j] = 0;
                    }
                }
            }
        }
    }
}
