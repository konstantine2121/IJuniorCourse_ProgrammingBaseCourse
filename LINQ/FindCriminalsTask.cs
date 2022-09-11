using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IJuniorCourse_ProgrammingBaseCourse.LINQ
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///У нас есть список всех преступников.<br/>
    ///В преступнике есть поля: ФИО, заключен ли он под стражу, рост, вес, национальность.<br/>
    ///Вашей программой будут пользоваться детективы.<br/>
    ///У детектива запрашиваются данные (рост, вес, национальность), <br/>
    ///и детективу выводятся все преступники, которые подходят под эти параметры,<br/>
    ///но уже заключенные под стражу выводиться не должны.
    /// </summary>
    class FindCriminalsTask : IRunnable
    {
        private CriminalsInfoContainer _infoContainer;

        #region IRunnable Implementation

        public void Run()
        {
            _infoContainer = new CriminalsInfoContainerCreator().Create();

            var working = true;

            while(working)
            {
                FindCriminal();
            }
        }
        #endregion IRunnable Implementation

        private Range ReadRange(string message)
        {
            var correct = false;

            Range range = new Range();

            while(correct == false)
            {
                ConsoleOutputMethods.Info(message);
                int from = ConsoleInputMethods.ReadPositiveInteger("Введите начальное значение - от: ");
                int to = ConsoleInputMethods.ReadPositiveInteger("Введите конечное значение - до: ");

                if (from <= to)
                {
                    correct = true;
                    range = new Range(from,to);
                }
                else
                {
                    ConsoleOutputMethods.Warning("Ошибка. Начальное значение должно быть меньше или равно конечному.");
                }
            }

            return range;
        }

        private void FindCriminal()
        {
            Console.Clear();
            ConsoleOutputMethods.Info("Идет поиск преступника.");
            ConsoleOutputMethods.WriteLine("Ввод данных: ");

            var height = ReadRange("Введите рост: ");
            var weight = ReadRange("Введите вес: ");
            
            _infoContainer.PrintAllNationalities();
            var nationality = ConsoleInputMethods.ReadString("Введите национальность: ");
            
            _infoContainer.SelectInfo(height, weight, nationality);
            
            Console.WriteLine("\nНажмите Enter чтобы ввести другой запрос.");
            Console.ReadLine();
        }

        #region Private Classes

        private class CriminalsInfoContainer
        {
            private const string FormatRecord = "{0, 20}  {1, 8}  {2, 8} {3, 8}  {4, 20}";

            private readonly List<CriminalRecord> _records;

            public CriminalsInfoContainer(IEnumerable<CriminalRecord> records)
            {
                _records = new List<CriminalRecord>();
                _records.AddRange(records);
            }

            public void SelectInfo(Range height, Range weight, string nationality)
            {
                Func<CriminalRecord, bool> checkFunction =
                    record =>
                    height.ContainsValue(record.Height)
                    && weight.ContainsValue(record.Weight)
                    && nationality.Equals(record.Nationality, StringComparison.OrdinalIgnoreCase)
                    && record.UnderArrest == false;

                var selectedRecords = _records.Where(record => checkFunction(record)).ToList();

                PrintSelectResult(selectedRecords);
            }

            public void PrintAllNationalities()
            {
                ConsoleOutputMethods.Info("Список доступных национальностей:");
                
                var nationalities =  _records.Select(record => record.Nationality).Distinct().ToList();                
                nationalities.Sort();

                nationalities.ForEach(record => Console.WriteLine(record));
                Console.WriteLine();
            }

            private void PrintSelectResult(List<CriminalRecord> records)
            {
                Console.WriteLine();
                ConsoleOutputMethods.Info("Найденная информация по запросу:  Всего записей : "+ records.Count());
                ConsoleOutputMethods.WriteLine(string.Format(FormatRecord, "ФИО", "Пойман", "Рост", "Вес", "Национальность"));

                records.ForEach(record => Console.WriteLine(FormatRecordInfo(record)));
            }

            private string FormatRecordInfo(CriminalRecord record)
            {
                return string.Format(FormatRecord,
                    record.FullName,
                    record.UnderArrest,
                    record.Height,
                    record.Weight,
                    record.Nationality);
            }
        }

        private class CriminalRecord
        {
            public CriminalRecord(string fullName, bool underArrest, int height, int weight, string nationality)
            {
                FullName = fullName;
                UnderArrest = underArrest;
                Height = height;
                Weight = weight;
                Nationality = nationality;
            }

            public string FullName { get; private set; }

            public bool UnderArrest { get; private set; }

            public int Height { get; private set; }

            public int Weight { get; private set; }

            public string Nationality { get; private set; }            
        }

        private struct Range
        {
            public int From;
            public int To;

            public Range(int from, int to)
            {
                From = from;
                To = to;
            }

            public bool Correct => From <= To;

            public bool ContainsValue(int value)
            {
                return From <= value && value <= To;
            }
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

        private class CriminalsInfoContainerCreator : RandomContainer, ICreator<CriminalsInfoContainer>
        {
            private const string NameTemplate = "Ф.И.О. ";
            private const string NationalityTemplate = "Нация";
            
            private const int MinWeight = 50;
            private const int MaxWeight = 150;
                                 
            private const int MinHeight = 140;
            private const int MaxHeight = 220;
                        
            private const int NumberOfNations = 9;
            private const int NumberOfCriminals = 200;

            private List<string> _nations;

            public CriminalsInfoContainer Create()
            {
                FillNationRecords();
                var criminals = CreateCriminals();

                return new CriminalsInfoContainer(criminals);
            }

            private void FillNationRecords()
            {
                _nations = new List<string>();

                for (int i=0; i< NumberOfNations; i++)
                {
                    _nations.Add(NationalityTemplate + (i + 1));
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
                var underArrest = Rand.Next(1 + 1) == 1;
                var height = Rand.Next(MinHeight, MaxHeight);
                var weight = Rand.Next(MinWeight, MaxWeight);
                var nation = _nations[Rand.Next(_nations.Count)];

                var criminal = new CriminalRecord(name, underArrest, height, weight, nation);

                return criminal;
            }
        }

        #endregion Creators

        #endregion Private Classes
    }
}
