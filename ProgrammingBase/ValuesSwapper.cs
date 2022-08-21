using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;

namespace IJuniorCourse_ProgrammingBaseCourse.ProgrammingBase
{
    /// <summary>
    ///Даны две переменные. Поменять местами значения двух переменных. Вывести на экран значения переменных до перестановки и после.<br/>
    ///К примеру, есть две переменные имя и фамилия, они сразу инициализированные, но данные не верные, перепутанные. Вот эти данные и надо поменять местами через код. 
    /// </summary>
    class ValuesSwapper : IRunnable
    {
        public void Run()
        {
            string firstName = "Иванов";
            string secondName = "Иван";

            Console.WriteLine("Значения переменных ДО перестановки.");
            PrintNameAndSurname(firstName, secondName);
                        
            string temp = secondName;
            secondName = firstName;
            firstName = temp;

            Console.WriteLine("\nЗначения переменных ПОСЛЕ перестановки.");
            PrintNameAndSurname(firstName, secondName);
        }

        /// <summary>
        /// Вывести на экран имя и фамилию.
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        private void PrintNameAndSurname(string name, string surname)
        {
            Console.WriteLine("Имя: {0}",name);
            Console.WriteLine("Фамилия: {0}", surname);
        }
    }
}
