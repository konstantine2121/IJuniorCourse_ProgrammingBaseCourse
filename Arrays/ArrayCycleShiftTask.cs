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
    ///Дан массив чисел. <br/>
    ///Нужно его сдвинуть циклически на указанное пользователем значение позиций влево,<br/>
    ///не используя других массивов. <br/>
    ///Пример для сдвига один раз: <br/>
    ///{1, 2, 3, 4} => {2, 3, 4, 1}<br/>

    /// </summary>
    class ArrayCycleShiftTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            int arrayLength = 5;
            //int[] array = new int[arrayLength];
            int[] array = {0, 1,2,3,4,5,6,7,8,9};

            int shift = 4;

            ConsoleOutputMethods.Info("Исходный массив.");
            PrintArray(array);
            Console.WriteLine("Сдвинуть влево на "+shift);
            
            ShiftArrayToLeft(array, shift);
            ConsoleOutputMethods.Info("Сдвинутый массив.");
            PrintArray(array);
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void ShiftArrayToLeft(int[] array,int shift)
        {
            int temp = 0;
            int nextIndex = 0;

            for (int i = 0; i < array.Length; i++)
            {
                nextIndex = GetNextIndexToLeft(i, shift, array.Length);
                temp = array[nextIndex];
                array[nextIndex] = array[i];

            }

            //array[GetNextIndexToLeft(0, shift, array.Length)] = temp;
            
        }

        private int GetNextIndexToLeft(int currentIndex, int shift, int arrayLength)
        {
            int firstIndex = 0;
            int result = (currentIndex - shift) % arrayLength;

            if (result < firstIndex)
            {
                result = arrayLength + result;
            }

            return result;
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
