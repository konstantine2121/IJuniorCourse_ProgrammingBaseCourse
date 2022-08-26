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
    ///Написать функцию, которая запрашивает число у пользователя (с помощью метода Console.ReadLine() ) и пытается сконвертировать его в тип int (с помощью int.TryParse())<br/>
    ///<br/>
    ///<br/>
    ///<br/>
    ///Если конвертация не удалась у пользователя запрашивается число повторно до тех пор, пока не будет введено верно. После ввода, который удалось преобразовать в число, число возвращается.<br/>
    ///<br/>
    ///P.S Задача решается с помощью циклов<br/>
    ///<br/>
    ///P.S Также в TryParse используется модфикатор параметра out<br/>
    /// </summary>
    class ParsingIntegerTask :IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            int value = ConsoleInputMethods.ReadInteger("Введите натурально число: ");

            ConsoleOutputMethods.Info("Вы ввели число "+value);

            Console.ReadKey();
        }

        #endregion IRunnable Implementation
    }
}
