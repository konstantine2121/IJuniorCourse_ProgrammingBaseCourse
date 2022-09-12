using System;
using System.Collections.Generic;
using System.Linq;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.LINQ
{
    /// <summary>
    /// Задача<br/>
    ///<br/>
    ///Есть набор тушенки. У тушенки есть название, год производства и срок годности.<br/>
    ///Написать запрос для получения всех просроченных банок тушенки.<br/>
    ///Чтобы не заморачиваться, можете думать, что считаем только года, без месяцев.
    /// </summary>
    class FindExpiredFoodTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            const int currentYear = 2022;

            var foodList = new FoodRecordCreator().Create();

            ConsoleOutputMethods.Info("Все запасы тушенки.");
            PrintRecords(foodList);

            //var expiredList = foodList.Where(record => record.CheckExpired(currentYear));
            var expiredList = foodList.Where(record => (record.DateOfProduction + record.ShelfLife) < currentYear);

            ConsoleOutputMethods.Info("Протухшие запасы тушенки.");
            PrintRecords(expiredList);

            Console.WriteLine("\nНажмите Enter чтобы выйти из программы.");
            Console.ReadLine();
        }

        #endregion IRunnable Implementation

        private void PrintRecords(IEnumerable<FoodRecord> records)
        {
            const string format = "{0, 20}  {1, 20}  {2, 20}";
            ConsoleOutputMethods.Info(string.Format(format, "Название", "Срок годности", "Год изготовления"));

            foreach (var record in records)
            {
                Console.WriteLine(format, record.FullName, record.ShelfLife, record.DateOfProduction);
            }
        }

        #region Private Classes

        private class FoodRecord
        {
            public FoodRecord(string fullName, int shelfLife, int dateOfProduction)
            {
                FullName = fullName;
                ShelfLife = shelfLife;
                DateOfProduction = dateOfProduction;
            }

            public string FullName { get; private set; }

            public int ShelfLife { get; private set; }

            public int DateOfProduction { get; private set; }

            public bool CheckExpired(int currentYear)
            {
                return (DateOfProduction + ShelfLife) < currentYear;
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

        private class FoodRecordCreator : RandomContainer, ICreator<List<FoodRecord>>
        {
            private const string NameTemplate = "Тушенка ";

            private const int MinDateOfProduction = 2018;
            private const int MaxDateOfProduction = 2022;

            private const int MinShelfLife = 1;
            private const int MaxShelfLife = 3;

            private const int NumberOfFood = 12;

            public List<FoodRecord> Create()
            {
                return CreatePlayer();
            }

            private List<FoodRecord> CreatePlayer()
            {
                var patients = new List<FoodRecord>();

                for (int i = 0; i < NumberOfFood; i++)
                {
                    patients.Add(CreateFood(i));
                }

                return patients;
            }

            private FoodRecord CreateFood(int index)
            {
                var name = NameTemplate + index;
                var shelfLife = Rand.Next(MinShelfLife, MaxShelfLife);
                var dateOfProduction = Rand.Next(MinDateOfProduction, MaxDateOfProduction);

                return new FoodRecord(name, shelfLife, dateOfProduction);
            }
        }

        #endregion Creators

        #endregion Private Classes
    }
}
