using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;

namespace IJuniorCourse_ProgrammingBaseCourse.Collections
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Есть два массива строк. <br/>
    ///Надо их объединить в одну коллекцию, исключив повторения, не используя Linq. <br/>
    ///Пример: {"1", "2", "1"} + {"3", "2"} => {"1", "2", "3"}
    /// </summary>
    class CombineStringArraysTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            string [] array1 = { "1", "2", "1" };
            string [] array2 = { "3", "2" };

            var result = CombineArrays(array1,array2);

            ConsoleOutputMethods.Info("Исходный массив 1:");
            PrintArray(array1);            
            ConsoleOutputMethods.Info("Исходный массив 2:");            
            PrintArray(array2);
            ConsoleOutputMethods.Info("Объединенный список:");
            PrintList(result);

            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private List<string> CombineArrays(string[] array1, string[] array2)
        {
            List<string> result = new List<string>();

            foreach (var line in array1)
            {
                if (result.Contains(line) == false)
                {
                    result.Add(line);
                }
            }            

            foreach (var line in array2)
            {
                if (result.Contains(line) == false)
                {
                    result.Add(line);
                }
            }            

            result.Sort();

            return result;
        }

        private void PrintList(List<string> list)
        {
            foreach (var line in list)
            {
                Console.WriteLine(line);
            }
        }

        private void PrintArray(string[] array)
        {
            foreach (var line in array)
            {
                Console.WriteLine(line);
            }
        }
    }
}
