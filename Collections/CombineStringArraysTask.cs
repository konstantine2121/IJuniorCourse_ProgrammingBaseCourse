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

            AddUniqueValuesToList(result, array1);
            AddUniqueValuesToList(result, array2);

            result.Sort();

            return result;
        }

        private void AddUniqueValuesToList(List<string> list, IEnumerable<string> newValues)
        {
            foreach (var line in newValues)
            {
                if (list.Contains(line) == false)
                {
                    list.Add(line);
                }
            }
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
