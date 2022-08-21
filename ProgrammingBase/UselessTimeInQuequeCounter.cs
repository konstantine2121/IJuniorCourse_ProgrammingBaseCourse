using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.ProgrammingBase
{
    /// <summary>
    /// ///Легенда:<br/>
    ///<br/>
    ///Вы заходите в поликлинику и видите огромную очередь из старушек, вам нужно рассчитать время ожидания в очереди.<br/>
    ///<br/>
    ///Формально:<br/>
    ///Пользователь вводит кол-во людей в очереди.<br/>
    ///Фиксированное время приема одного человека всегда равно 10 минутам.<br/>
    ///Пример ввода: Введите кол-во старушек: 14<br/>
    ///Пример вывода: "Вы должны отстоять в очереди 2 часа и 20 минут." 
    /// </summary>
    class UselessTimeInQuequeCounter : IRunnable
    {
        public void Run()
        {
            const int TimeForOneGranny = 10;
            const int MinutesInHours = 60;
            const string QuestionQuequeLength = "Введите кол-во старушек: ";

            int quequeLength = ReadIntValue(QuestionQuequeLength);

            int totalTimeInMinutes = quequeLength * TimeForOneGranny;

            int hoursToWait = totalTimeInMinutes / MinutesInHours;
            int minutesInLastHourToWait = totalTimeInMinutes % MinutesInHours;

            var resultBuilder = new StringBuilder();

            resultBuilder.Append("Вы должны отстоять в очереди ");
            
            if (hoursToWait > 0)
            {
                resultBuilder.Append($"{hoursToWait} часа(ов) и ");
            }

            resultBuilder.AppendLine($"{minutesInLastHourToWait} минут.");

            Console.WriteLine(resultBuilder.ToString());
        }

        private int ReadIntValue(string message)
        {
            int result = 0;
            bool parsed = false;

            while (parsed == false)
            {
                Console.Write(message);
                var input = Console.ReadLine();
                parsed = int.TryParse(input, out result);

                if (parsed == false)
                {
                    Console.WriteLine("Не получилось распознать значение. Попробуйте еще раз.");
                }
                else
                {
                    if (result < 0)
                    {
                        Console.WriteLine("Значение не может быть отрицательным. Попробуйте еще раз.");
                    }
                }
            }

            return result;
        }
    }
}
