using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.ProgrammingBase;
using IJuniorCourse_ProgrammingBaseCourse.СonditionsAndCycles;
using System;
using System.Collections.Generic;

namespace IJuniorCourse_ProgrammingBaseCourse
{
    class Program
    {
        private static Dictionary<int, IRunnable> programmingBaseTasks =
            new Dictionary<int, IRunnable>(){
                {1, new Variables()},
                {2, new IntegerDivisionTask()},
                {3, new SurveyTask()},
                {4, new ImagesInRow()},
                {5, new ValuesSwapper()},
                {6, new BuySomeCrystals() },
                {7, new UselessTimeInQuequeCounter() }
            };

        private static Dictionary<int, IRunnable> conditionsAndCyclesTasks =
            new Dictionary<int, IRunnable>(){
                {1, new CycledMessageTask()}         
            };

        private static void Main(string[] args)
        {
            //RunTask(7, programmingBaseTasks);
            RunTask(1, conditionsAndCyclesTasks); 
        }

        private static void RunTask(int taskNumber, Dictionary<int, IRunnable> tasks)
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;

            if (tasks.ContainsKey(taskNumber))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Выполнение задачи под номером {taskNumber}.");
                Console.ForegroundColor = ConsoleColor.White;
                tasks[taskNumber].Run();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Задача с указанным номером '{taskNumber}' не найдена.");
            }

            Console.ForegroundColor = foregroundColor;
        }
    }
}
