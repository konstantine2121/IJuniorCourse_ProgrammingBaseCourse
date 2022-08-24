using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.ProgrammingBase;
using IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles;
using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.Arrays;

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
                {1, new CycledMessageTask()},
                {2, new ExitViaConditionTask() },
                {3, new SequencePrinter() },
                {4, new SumOfMultiplicityNumbers() },
                {5, new СurrencyConverter() },
                {6, new ConsoleCommanderTask() },
                {7, new NameInFrameTask() },
                {8, new PasswordCheckTask() },
                {9, new CountingNaturalNumbersTask() },
                {10, new ExponentCounterTask() },
                {11, new StringCheckTask() },
                {12, new WizardBossFightTask() }
            };

        private static Dictionary<int, IRunnable> arraysTasks =
            new Dictionary<int, IRunnable>(){
                {1, new CountRowSumAndColumnMultiplicationTask()},
                {2, new FindingMaxValueInMatrixTask()},
                {3, new FindingLocalExtremumsTask() }
            };

        private static void Main(string[] args)
        {
            //RunTask(7, programmingBaseTasks);
            //RunTask(12, conditionsAndCyclesTasks);            
            RunTask(3, arraysTasks);
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
