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
    ///Дан двумерный массив.<br/>
    ///Вычислить сумму второй строки и произведение первого столбца. Вывести исходную матрицу и результаты вычислений.<br/>
    /// </summary>
    class CountRowSumAndColumnMultiplicationTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            int[,] array = {
                { 1,2,3,4},
                { 1,2,3,4},
                { 1,2,3,4},
                { 1,2,3,4}
            };

            int indexOfRow = 1;
            int indexOfColumn = 0;

            int summResult = 0;
            int multiplicationResult = 1;

            for (int i=0; i < array.GetLength(1);i++)
            {
                summResult += array[ indexOfRow,i];
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                multiplicationResult *= array[i, indexOfColumn];
            }

            PrintArray(array);

            Console.WriteLine();
            Console.WriteLine("Cумма второй строки = "+ summResult);
            Console.WriteLine("Произведение первого столбца = " + multiplicationResult);
        }

        #endregion IRunnable Implementation

        private void PrintArray(int [,] array)
        {
            Console.WriteLine();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write("{0, 5}\t",array[i,j]);
                }
                Console.WriteLine();
            }

        }
    }
}
