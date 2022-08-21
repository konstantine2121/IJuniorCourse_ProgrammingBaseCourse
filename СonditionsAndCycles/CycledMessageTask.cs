using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;

namespace IJuniorCourse_ProgrammingBaseCourse.СonditionsAndCycles
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///При помощи циклов вы можете повторять один и тот же код множество раз.<br/>
    ///Напишите простейшую программу, которая выводит указанное(установленное) пользователем сообщение заданное количество раз. Количество повторов также должен ввести пользователь.<br/>
    /// </summary>
    class CycledMessageTask :IRunnable
    {
       public void Run()
        {
            const string QuestionMessage = "Укажите сообщение для вывода:\n";
            const string QuestionNumber = "Укажите количество повторов: ";

            string message = ConsoleInputMethods.ReadString(QuestionMessage);
            int numberOfCycles = ConsoleInputMethods.ReadPositiveInteger(QuestionNumber);

            Console.WriteLine();

            for (int i = 0; i < numberOfCycles; i++)
            {
                Console.WriteLine(message);
            }
        }
    }
}
