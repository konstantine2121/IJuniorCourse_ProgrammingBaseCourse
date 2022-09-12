using System;
using System.Collections.Generic;
using System.Linq;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.LINQ
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///В нашей великой стране Арстоцка произошла амнистия!<br/>
    ///Всех людей, заключенных за преступление "Антиправительственное", следует исключить из списка заключенных.<br/>
    ///Есть список заключенных, каждый заключенный состоит из полей: ФИО, преступление.<br/>
    ///Вывести список до амнистии и после.
    /// </summary>
    class AmnestyTask : IRunnable
    {
        private const string Format = "{0, 20}  {1,20}";

        #region IRunnable Implementation

        public void Run()
        {
            var criminals = new CriminalRecordListCreator().Create();

            ConsoleOutputMethods.WriteLine("Перечень до амнистии.", ConsoleColor.Cyan);
            PrintCriminals(criminals);

            criminals = criminals.Where(record => record.ArrestReason.Equals(CriminalRecordListCreator.AntigovernmentReason, StringComparison.OrdinalIgnoreCase) == false).ToList();

            ConsoleOutputMethods.WriteLine("Перечень после амнистии.", ConsoleColor.Cyan);
            PrintCriminals(criminals);

            Console.WriteLine("\nНажмите Enter чтобы выйти из программы.");
            Console.ReadLine();
        }

        #endregion IRunnable Implementation

        private void PrintCriminals(List<CriminalRecord> criminals)
        {
            ConsoleOutputMethods.Info(string.Format(Format, "ФИО", "Причина"));
            criminals.ForEach(record => Console.WriteLine(Format, record.FullName, record.ArrestReason));
            Console.WriteLine();
        }

        #region Private Classes

        private class CriminalRecord
        {
            public CriminalRecord(string fullName, string arrestReason)
            {
                FullName = fullName;
                ArrestReason = arrestReason;
            }

            public string FullName { get; private set; }

            public string ArrestReason { get; private set; }
        }

        #region Creators

        private interface ICreator<T>
        {
            T Create();
        }

        private class RandomContainer
        {
            protected readonly Random Rand = new Random();
        }

        private class CriminalRecordListCreator : RandomContainer, ICreator<List<CriminalRecord>>
        {
            public const string AntigovernmentReason = "Антиправительственное";
            private const string NameTemplate = "Ф.И.О. ";
            private const string ReasonTemplate = "Какая-то";

            private const int NumberOfCriminals = 60;
            private const int NumberOfReasons = 4;

            private List<string> _reasons;

            public List<CriminalRecord> Create()
            {
                FillReasons();
                var criminals = CreateCriminals();

                return criminals;
            }

            private void FillReasons()
            {
                _reasons = new List<string>();
                _reasons.Add(AntigovernmentReason);

                for (int i = 0; i < NumberOfReasons; i++)
                {
                    _reasons.Add(ReasonTemplate +(i+1));
                }
            }

            private List<CriminalRecord> CreateCriminals()
            {
                var criminals = new List<CriminalRecord>();

                for (int i = 0; i < NumberOfCriminals; i++)
                {
                    criminals.Add(CreateCriminal(i));
                }

                return criminals;
            }

            private CriminalRecord CreateCriminal(int index)
            {
                var name = NameTemplate + (index + 1);
                var reason = _reasons[Rand.Next(_reasons.Count)];

                return new CriminalRecord(name, reason);
            }
        }

        #endregion Creators

        #endregion Private Classes

    }
}
