using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IJuniorCourse_ProgrammingBaseCourse.LINQ
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///У вас есть список больных(минимум 10 записей)<br/>
    ///Класс больного состоит из полей: ФИО, возраст, заболевание.<br/>
    ///Требуется написать программу больницы, в которой перед пользователем будет меню со следующими пунктами:<br/>
    ///1)Отсортировать всех больных по фио<br/>
    ///2)Отсортировать всех больных по возрасту<br/>
    ///3)Вывести больных с определенным заболеванием<br/>
    ///(название заболевания вводится пользователем с клавиатуры) 
    /// </summary>
    class PatientsSortTask : IRunnable
    {
        private List<PatientRecord> _patients;

        private enum MenuActions
        {
            SortByName = 1,
            SortByAge,
            SelectByDisease
        }

        #region IRunnable Implementation

        public void Run()
        {
            _patients = new PatientRecordCreator().Create();

            var working = true;

            while(working)
            {
                Console.Clear();
                var action = ReadMenuAction();
                
                switch(action)
                {
                    case MenuActions.SortByName:
                        SortByName();
                        break;
                    case MenuActions.SortByAge:
                        SortByAge();
                        break;
                    case MenuActions.SelectByDisease:
                        SelectByDisease();
                        break;
                }
            }

            Console.WriteLine("\nНажмите Enter чтобы выйти из программы.");
            Console.ReadLine();
        }

        #endregion IRunnable Implementation

        private MenuActions ReadMenuAction()
        {
            var action = MenuActions.SelectByDisease;

            var correctInputValues = Enum.GetValues(typeof(MenuActions)).Cast<MenuActions>().ToList();
            var message = $"Что вы желаете сделать?\nСортировка по имени - {(int)MenuActions.SortByName}, сортировка по возрасту - {(int)MenuActions.SortByAge}, Провести поиск по болезни - {(int)MenuActions.SelectByDisease}\nВведите : ";

            var parsed = false;

            while (parsed == false)
            {
                var input = (MenuActions) ConsoleInputMethods.ReadPositiveInteger(message);

                if (correctInputValues.Contains(action))
                {
                    action = input;
                    parsed = true;
                }
                else
                {
                    ConsoleOutputMethods.Warning("Такой опции нет в списке.");
                }
            }

            return action;
        }

        private void SortByName()
        {
            var sorted = _patients.ToList().OrderBy(record => record.FullName);

            Console.WriteLine("Список пациентов, отсортированный по имени.");
            PrintRecords(sorted);

            EndAction();
        }

        private void SortByAge()
        {
            var sorted = _patients.OrderBy(record => record.Age);

            Console.WriteLine("Список пациентов, отсортированный по возрасту.");
            PrintRecords(sorted);

            EndAction();
        }

        private void SelectByDisease()
        {

            var diseases = _patients
                .Select(record => record.Disease)
                .Distinct()
                .OrderBy(record => record)
                .ToList();

            Console.WriteLine("Список болезней у пациентов.");
            diseases.ForEach(disease => Console.WriteLine(disease));
            Console.WriteLine();

            var input = ConsoleInputMethods.ReadString("Введите название болезни: ");

            var selected = _patients.Where(record => record.Disease.Equals(input, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("Список пациентов c указанной болезнью.");
            PrintRecords(selected);

            EndAction();
        }

        private void EndAction()
        {
            Console.WriteLine("\nНажмите Enter для возврата в меню.");
            Console.ReadLine();
        }

        private void PrintRecords(IEnumerable<PatientRecord> records)
        {
            const string format = "{0, 20}  {1, 8}  {2, 8}";
            ConsoleOutputMethods.Info(string.Format(format, "Имя", "Возраст", "Болезнь"));

            foreach(var record in records)
            {
                Console.WriteLine(format, record.FullName, record.Age, record.Disease);
            }
        }

        #region Private Classes

        private class PatientRecord
        {
            public PatientRecord(string fullName, int age, string disease)
            {
                FullName = fullName;
                Age = age;
                Disease = disease;
            }

            public string FullName { get; private set; }

            public int Age { get; private set; }

            public string Disease { get; private set; }
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

        private class PatientRecordCreator : RandomContainer, ICreator<List<PatientRecord>>
        {
            private const string NameTemplate = "Ф.И.О. ";
            private const string DiseaseTemplate = "Болезнь";

            private const int MinAge = 15;
            private const int MaxAge = 75;

            private const int NumberOfPatients = 10;
            private const int NumberOfReasons = 4;

            private List<string> _reasons;

            public List<PatientRecord> Create()
            {
                FillReasons();
                var patients = CreatePatients();

                return patients;
            }

            private void FillReasons()
            {
                _reasons = new List<string>();

                for (int i = 0; i < NumberOfReasons; i++)
                {
                    _reasons.Add(DiseaseTemplate + (i + 1));
                }
            }

            private List<PatientRecord> CreatePatients()
            {
                var patients = new List<PatientRecord>();

                for (int i = 0; i < NumberOfPatients; i++)
                {
                    patients.Add(CreatePatient(i));
                }

                return patients;
            }

            private PatientRecord CreatePatient(int index)
            {
                var name = NameTemplate + index;
                var age = Rand.Next(MinAge, MaxAge);
                var reason = _reasons[Rand.Next(_reasons.Count)];

                return new PatientRecord(name, age, reason);
            }
        }

        #endregion Creators
        
        #endregion Private Classes
    }
}
