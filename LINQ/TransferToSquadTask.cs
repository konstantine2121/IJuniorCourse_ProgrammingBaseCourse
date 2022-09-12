using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.LINQ
{
    /// <summary>
    /// ///Задача<br/>
    ///<br/>
    ///Есть 2 списка в солдатами.<br/>
    ///Всех бойцов из отряда 1, у которых фамилия начинается на букву Б, требуется перевести в отряд 2.
    /// </summary>
    class TransferToSquadTask : IRunnable
    {
        private List<Soldier> _firstSquad;
        private List<Soldier> _secondSquad;

        #region IRunnable Implementation

        public void Run()
        {
            _firstSquad = CreateFirstSquad();
            _secondSquad = CreateSecondSquad();

            ConsoleOutputMethods.Info("\nСписки отрядов до перевода.\n");
            PrintSquadsInfo();

            MakeTransfer();

            ConsoleOutputMethods.Info("\nСписки отрядов после перевода.\n");
            PrintSquadsInfo();

            Console.WriteLine("\nНажмите Enter чтобы выйти из программы.");
            Console.ReadLine();
        }

        #endregion IRunnable Implementation

        private void MakeTransfer()
        {
            const string transferCondiion = "Б";

            var transferSoldiers = _firstSquad
                .Where(record => 
                record.Surname.ToUpper().StartsWith(
                   transferCondiion.ToUpper()) )
                .ToList();

            ConsoleOutputMethods.Info("Список солдат для перевода.\n");
            PrintSquadInfo(transferSoldiers);

            _firstSquad = _firstSquad.Except(transferSoldiers).ToList();

            _secondSquad = _secondSquad.Union(transferSoldiers).ToList();
        }

        private void PrintSquadsInfo()
        {
            ConsoleOutputMethods.WriteLine("Информация по первому отряду.", ConsoleColor.Cyan);
            PrintSquadInfo(_firstSquad);
            ConsoleOutputMethods.WriteLine("Информация по второму отряду.", ConsoleColor.Cyan);
            PrintSquadInfo(_secondSquad);
        }

        private void PrintSquadInfo(IEnumerable<Soldier> soldiers)
        {
            ConsoleOutputMethods.Info("Фамилия");
            
            foreach(var soldier in soldiers)
            {
                Console.WriteLine(soldier.Surname);
            }
            Console.WriteLine();
        }

        private List<Soldier> CreateFirstSquad()
        {
            var squad = new List<Soldier>();

            squad.Add(new Soldier("Антонов"));
            squad.Add(new Soldier("Борисов"));
            squad.Add(new Soldier("Иванов"));
            squad.Add(new Soldier("Петров"));
            squad.Add(new Soldier("Сидоров"));
            squad.Add(new Soldier("Бобров"));
            squad.Add(new Soldier("Курочкин"));

            return squad;
        }

        private List<Soldier> CreateSecondSquad()
        {
            var squad = new List<Soldier>();
                        
            squad.Add(new Soldier("Куроедов"));

            return squad;
        }

        #region PrivateClasses

        private class Soldier
        {
            public Soldier(string surname)
            {
                Surname = surname;
            }

            public string Surname { get; private set; }
        }

        #endregion PrivateClasses
    }

}
