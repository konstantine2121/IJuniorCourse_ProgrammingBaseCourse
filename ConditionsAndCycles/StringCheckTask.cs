using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Дана строка из символов '(' и ')'. Определить, является ли она корректным скобочным выражением. Определить максимальную глубину вложенности скобок.<br/>
    ///<br/>
    ///Пример “(()(()))” - строка корректная и максимум глубины равняется 3.<br/>
    ///Пример не верных строк: "(()", "())", ")(", "(()))(()"<br/>
    ///<br/>
    ///Для перебора строки по символам можно использовать цикл foreach, к примеру будет так foreach (var symbol in text)<br/>
    ///Или цикл for(int i = 0; i &lt; text.Length; i++) и дальше обращаться к каждому символу внутри цикла как text[i]<br/>
    ///Цикл нужен для перебора всех символов в строке. 
    /// </summary>
    class StringCheckTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            const char LeftBracketChar = '(';
            const char RightBracketChar = ')';

            var maxDepth = 0;
            var currentDepth = 0;

            var input = ConsoleInputMethods.ReadString("Введите строку состоящую из одних скобок: ");

            var correctString = input.All(character => character == LeftBracketChar || character == RightBracketChar);

            if (correctString == false)
            {
                ConsoleOutputMethods.Warning("В строке присутствуют посторонние символы");
                Console.ReadKey();
                return;
            }

            var outOfBound = false;

            foreach (var character in input)
            {
                if (character == LeftBracketChar)
                {
                    currentDepth++;
                }
                else 
                {
                    currentDepth--;
                }

                if (currentDepth > maxDepth)
                {
                    maxDepth = currentDepth;
                }

                if (currentDepth < 0)
                {
                    outOfBound = true;
                }
            }

            var lineIsCorrect = currentDepth == 0 && outOfBound == false;

            if (lineIsCorrect)
            {
                ConsoleOutputMethods.Info(
                    string.Format("'{0}' - строка корректная и максимум глубины равняется {1}.", input, maxDepth));
            }
            else
            {
                ConsoleOutputMethods.Warning(string.Format("'{0}' - строка НЕ корректная.", input));
            }

            Console.ReadKey();
        }

        #endregion IRunnable Implementation
    }
}
