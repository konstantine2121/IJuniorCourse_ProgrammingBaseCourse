using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.ProgrammingBase;
using IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles;
using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.Arrays;
using IJuniorCourse_ProgrammingBaseCourse.Functions;
using IJuniorCourse_ProgrammingBaseCourse.Collections;
using IJuniorCourse_ProgrammingBaseCourse.OOP;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Creators;

namespace IJuniorCourse_ProgrammingBaseCourse
{
    class Program
    {
        private static Dictionary<int, IRunnable> programmingBaseTasks =
            new Dictionary<int, IRunnable>()
            {
                {1, new Variables()},
                {2, new IntegerDivisionTask()},
                {3, new SurveyTask()},
                {4, new ImagesInRow()},
                {5, new ValuesSwapper()},
                {6, new BuySomeCrystals() },
                {7, new UselessTimeInQuequeCounter() }
            };

        private static Dictionary<int, IRunnable> conditionsAndCyclesTasks =
            new Dictionary<int, IRunnable>()
            {
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
            new Dictionary<int, IRunnable>()
            {
                {1, new CountRowSumAndColumnMultiplicationTask()},
                {2, new FindingMaxValueInMatrixTask()},
                {3, new FindingLocalExtremumsTask() },
                {4, new Arrays.SummConsoleInputTask() },
                {5, new FindingTheLongestSubarrayTask() },
                {6, new BubbleSortingTask() },
                {7, new StringSplitTask() },
                {8, new ArrayCycleShiftTask() }
            };

        private static Dictionary<int, IRunnable> functionsTasks =
            new Dictionary<int, IRunnable>()
            {
                {1, new Functions.ResordsStorageTask()},
                {2, new DrawHealthbarTask()},
                {3, new ParsingIntegerTask()},
                {4, new GameMapTask()},
                {5, new ShuffleTask()}
            };

        private static Dictionary<int, IRunnable> collectionsTasks =
            new Dictionary<int, IRunnable>()
            {
                {1, new ExplanatoryDictionaryTask()},
                {2, new QueueTask()},
                {3, new Collections.SummConsoleInputTask()},
                {4, new Collections.ResordsStorageTask()},
                {5, new CombineStringArraysTask()}
            };

        private static Dictionary<int, IRunnable> oopTasks =
            new Dictionary<int, IRunnable>()
            {
                {-1, new PlayersModuleCreator().Create() },
                {1, new WorkingWithClassesTask() },
                {2, new WorkingWithPropertiesTask() },
                {3, new PlayersDatabaseTask() },
                {4, new PackOfPlayingCardsTask() },
                {5, new BookStorageTask() },
                {6, new ShopTask() }
            };

        private static void Main(string[] args)
        {
            //RunTask(7, programmingBaseTasks);
            //RunTask(12, conditionsAndCyclesTasks);            
            //RunTask(8, arraysTasks);
            //RunTask(2, functionsTasks);
            //RunTask(5, collectionsTasks);
            RunTask(4, oopTasks);
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
