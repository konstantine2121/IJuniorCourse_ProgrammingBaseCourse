using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.Functions
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Реализуйте функцию Shuffle, которая перемешивает элементы массива в случайном порядке.<br/>
    /// </summary>
    class ShuffleTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            int arrayLength = 20;
            int[] array = new int[arrayLength];

            InitArrayWithIncreasingValues(array);

            ConsoleOutputMethods.Info("Исходный массив.");
            PrintArray(array);

            Shuffle(array);
            ConsoleOutputMethods.Info("Перемешанный массив.");
            PrintArray(array);
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void Shuffle(int [] array)
        {
            Random random = new Random();
            int index1 = 0;
            int index2 = 0;
            int temp = 0;

            int repeatTimes = array.Length * 10;

            for (int i = 0; i < repeatTimes; i++)
            {
                index1 = random.Next(array.Length);

                do 
                {
                    index2 = random.Next(array.Length);
                }
                while (index1 == index2);

                temp = array[index1];
                array[index1] = array[index2];
                array[index2] = temp;
            }
        }

        private void InitArrayWithIncreasingValues(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
        }
        
        private void PrintArray(int[] array)
        {
            Console.WriteLine();

            int numberInRow = 10;

            for (int i = 0; i < array.Length; i++)
            {
                if (i != 0 && i % numberInRow == 0)
                {
                    Console.WriteLine();
                }

                Console.Write("{0, 4}  ", array[i]);
            }

            Console.WriteLine();
        }


    }
}
