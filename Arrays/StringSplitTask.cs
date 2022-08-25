using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.Arrays
{
    /// <summary>
    /// ///Задача<br/>
    ///<br/>
    ///Дана строка с текстом, используя метод строки String.Split() получить массив слов, которые разделены пробелом в тексте и вывести массив, каждое слово с новой строки.
    /// </summary>
    class StringSplitTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            string text = "Дана строка с текстом, используя метод строки String.Split() получить массив слов, которые разделены пробелом в тексте и вывести массив, каждое слово с новой строки.";

            string[] lines = text.Split(' ');

            Console.WriteLine("Исходная строка.\n\n"+text);

            Console.WriteLine("\nСлова из строки:");

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        #endregion IRunnable Implementation
    }
}
