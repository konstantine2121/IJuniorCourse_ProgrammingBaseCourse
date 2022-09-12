using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.LINQ
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Существует класс солдата. <br/>
    ///В нём есть поля: <br/>
    ///имя, вооружение, звание, срок службы(в месяцах).<br/>
    ///Написать запрос, при помощи которого получить набор данных состоящий из имени и звания.<br/>
    ///Вывести все полученные данные в консоль.<br/>
    ///(Не менее 5 записей)
    /// </summary>
    class SoldierInfoTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            const string format = "{0, 20}  {1, 20}";

            var soldiers = new SoldierRecordListCreator().Create();

            var selectedInfo = soldiers.Select(record => new{Name = record.FullName, Rank = record.Rank }).ToList();

            ConsoleOutputMethods.Info(string.Format(format,"Имя", "Звание"));
            selectedInfo.ForEach(record => Console.WriteLine(format,record.Name, record.Rank));

            Console.WriteLine("\nНажмите Enter чтобы выйти из программы.");
            Console.ReadLine();
        }

        #endregion IRunnable Implementation

        

        #region PrivateClasses

        private class SoldierRecord
        {
            public SoldierRecord(string fullName, string weapon, string rank, int militaryServiceTerm)
            {
                FullName = fullName;
                Weapon = weapon;
                Rank = rank;
                MilitaryServiceTerm = militaryServiceTerm;
            }

            public string FullName { get; private set; }

            public string Weapon { get; private set; }

            public string Rank { get; private set; }

            /// <summary>
            /// Выслуга в месяцах
            /// </summary>
            public int MilitaryServiceTerm{ get; private set; }
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

        private class SoldierRecordListCreator : RandomContainer, ICreator<List<SoldierRecord>>
        {
            private const string NameTemplate = "Ф.И.О. ";
            private const string WeaponTemplate = "Оружие ";
            private const string RankTemplate = "Звание ";
            
            private const int MinServiceTerm = 1;
            private const int MaxServiceTerm = 16;
            
            private const int NumberOfSoldiers = 10;
            private const int NumberOfWeapons = 40;
            private const int NumberOfRanks = 20;

            private List<string> _weapons;
            private List<string> _ranks;

            public SoldierRecordListCreator()
            {
                Initialize();
            }

            public List<SoldierRecord> Create()
            {
                return CreateSoldiers();
            }

            private void Initialize()
            {
                FillWeapons();
                FillRanks();
            }

            private void FillWeapons()
            {
                _weapons = new List<string>();
                FillList(_weapons, NumberOfWeapons, WeaponTemplate);
            }

            private void FillRanks()
            {
                _ranks = new List<string>();
                FillList(_ranks, NumberOfRanks, RankTemplate);
            }

            private void FillList(List<string> list, int numberOfRecords, string template)
            {
                for (int i = 0;i < numberOfRecords; i++)
                {
                    list.Add(template + (i + 1));
                }
            }

            private List<SoldierRecord> CreateSoldiers()
            {
                var soldiers = new List<SoldierRecord>();

                for (int i = 0; i < NumberOfSoldiers; i++)
                {
                    soldiers.Add(CreateSoldier(i));
                }
                return soldiers;
            }

            private SoldierRecord CreateSoldier(int index)
            {
                var name = NameTemplate + (index + 1);
                var weapon = _weapons[Rand.Next(_weapons.Count)];
                var rank = _ranks[Rand.Next(_ranks.Count)];
                var serviceTerm = Rand.Next(MinServiceTerm, MaxServiceTerm);

                return new SoldierRecord(name, weapon, rank, serviceTerm);
            }
        }

        #endregion Creators

        #endregion PrivateClasses
    }
}
