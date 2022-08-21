using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Text;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    /// <summary>
    /// ///<br/>
    ///Задача<br/>
    ///<br/>
    ///Нужно написать программу (используя циклы, обязательно пояснить выбор вашего цикла), чтобы она выводила следующую последовательность 5 12 19 26 33 40 47 54 61 68 75 82 89 96<br/>
    ///Нужны переменные для обозначения чисел в условии цикла.<br/>    
    /// </summary>
    class SequencePrinter : IRunnable
    {
        public void Run()
        {
            const string Answer = @"
Глядя на эту последовательность
5 12 19 26 33 40 47 54 61 68 75 82 89 96
сразу бросается в глаза, что между элементами происходит
приращение на одно и то же значение - +7.
Начальное значение последовательности  = 5,
Всего 14 элементов в последовательности.
";
            //const string Sequence = "5 12 19 26 33 40 47 54 61 68 75 82 89 96";

            const int StartValue = 5;
            const int EndValue = 96;
            const int DeltaIncrement = 7;
            
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = StartValue; i <= EndValue; i += DeltaIncrement)
            {   
                stringBuilder.Append(i + " ");
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            Console.WriteLine("Искомая последовательность:\n" + stringBuilder.ToString());
        }
    }
}
