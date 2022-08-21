using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Написать программу, которая будет выполняться до тех пор, пока не будет введено слово exit.<br/>
    ///Помните, в цикле должно быть условие, которое отвечает за то, когда цикл должен завершиться.<br/>
    ///Это нужно, чтобы любой разработчик взглянув на ваш код, понял четкие границы вашего цикла.<br/>
    ///
    /// </summary>
    class ExitViaConditionTask : IRunnable
    {
        public void Run()
        {
            const string InfoMessage = "Введите какой нибудь текст(exit для выхода):\n";
            const string ExitCommand = "exit";
            
            string input = string.Empty;

            bool stopped = false;

            while (stopped == false)
            {
                input = ConsoleInputMethods.ReadString(InfoMessage);
                Console.WriteLine("Вы ввели: "+ input);

                if (input.Equals(ExitCommand))
                {
                    stopped = true;
                }
            }

            Console.WriteLine("Выход из программы.");
            Console.ReadKey();
        }
    }
}
