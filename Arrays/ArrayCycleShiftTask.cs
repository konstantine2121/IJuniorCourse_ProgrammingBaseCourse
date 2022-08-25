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
            int arrayLength =7;

            int shift = 6;

            int[] array = new int [arrayLength];

            InitArrayWithIncreasingValues(array);

            ConsoleOutputMethods.Info("Исходный массив.");
            PrintArray(array);
            Console.WriteLine("Сдвинуть влево на "+shift);
            
            ShiftArrayToLeft(array, shift);
            ConsoleOutputMethods.Info("Сдвинутый массив.");
            PrintArray(array);
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void ShiftArrayToLeft(int[] array, int shift)
        {
            if (array == null || array.Length == 0)
            {
                return;
            }

            int temp = array[0];
            int startIndex = 0;

            int checkDivider = -1;
            bool forcedShiftCheck = array.Length % shift == 0;

            if (forcedShiftCheck)
            {
                checkDivider = array.Length / shift;
            }

            for (int i = 0; i < array.Length; i++)
            {
                int valueToInsert = temp;
                int nextIndex = GetNextIndexToLeft(startIndex, shift, array.Length);

                temp = array[nextIndex];
                array[nextIndex] = valueToInsert;

                if (forcedShiftCheck == false)
                {
                    startIndex = nextIndex;
                }
                else
                {
                    if ((i + 1) % checkDivider == 0) //Избежать петлю
                    {
                        startIndex = nextIndex + 1;

                        if (startIndex >=array.Length)
                        {
                            startIndex = 0;
                        }

                        temp = array[startIndex];
                    }
                    else
                    {
                        startIndex = nextIndex;
                    }
                }
            }
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

        private void InitArrayWithIncreasingValues(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
        }
    }
}
