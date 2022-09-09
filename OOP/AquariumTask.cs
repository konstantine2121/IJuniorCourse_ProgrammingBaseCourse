using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.CommonViews;


namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Есть аквариум, в котором плавают рыбы. В этом аквариуме может быть максимум определенное кол-во рыб. Рыб можно добавить в аквариум или рыб можно достать из аквариума. <br/>
    ///(программу делать в цикле для того, чтобы рыбы могли “жить”)<br/>
    ///Все рыбы отображаются списком, у рыб также есть возраст. <br/>
    ///За 1 итерацию рыбы стареют на определенное кол-во жизней и могут умереть. <br/>
    ///Рыб также вывести в консоль, чтобы можно было мониторить показатели.
    /// </summary>
    class AquariumTask : IRunnable
    {
        private ConsoleRecord _totalDayPastStatusBar;
        private ConsoleRecord _previousCommandStatusBar;        
        private ConsoleTable _aquariumInfoBar;

        private AquariumInfoPrinter _aquariumInfoPrinter;
        private Aquarium _aquarium;

        private int _currentDay = 1;

        private enum ActionOption
        {
            AddNewFish = 1,
            RemoveFish,
            SkipDay
        }

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();

            //В условиях задачи не оговорено, когда закончить цикл.
            bool playerIsAlive = true;

            while(playerIsAlive)
            {
                bool endDay = false;

                while(endDay == false)
                {
                    UpdateInfoBars();
                    var action = ReadActionOption();

                    switch(action)                        
                    {
                        case ActionOption.SkipDay:
                            endDay = true;
                            SkipDay();
                            break;

                        case ActionOption.AddNewFish:
                            AddNewFish();
                            break;

                        case ActionOption.RemoveFish:
                            RemoveFish();
                            break;
                    }

                }
            }
          
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void Initialize()
        {
            _currentDay = 1;

            int verticalOffset = 12;

            _totalDayPastStatusBar = new ConsoleRecord(40, 0);
            _totalDayPastStatusBar.ForegroundColor = ConsoleColor.Cyan;
            
            _aquariumInfoBar = new ConsoleTable(0, 1);
            
            _previousCommandStatusBar = new ConsoleRecord(0, verticalOffset);
            
            _aquariumInfoPrinter = new AquariumInfoPrinter();
                        
            _aquarium = new AquariumCreator().Create();
        }

        private void UpdateInfoBars()
        {
            Console.Clear();

            _totalDayPastStatusBar.Text = "Текущий день "+ _currentDay;
            _totalDayPastStatusBar.Update();

            _aquariumInfoPrinter.Print(_aquarium, _aquariumInfoBar);
            
            _previousCommandStatusBar.Update();
            
            Console.WriteLine();
        }

        private ActionOption ReadActionOption()
        {
            Console.WriteLine();

            string formatCommand = "  {0,-16} - {1, 2}\n";

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat(formatCommand, "Добавить рыбку", (int)ActionOption.AddNewFish);
            stringBuilder.AppendFormat(formatCommand, "Убрать рыбку", (int)ActionOption.RemoveFish);
            stringBuilder.AppendFormat(formatCommand, "Закончить день", (int)ActionOption.SkipDay);

            ConsoleOutputMethods.Info("Что вы желаете сделать?: ");
            Console.WriteLine(stringBuilder.ToString());

            bool parsed = false;
            ActionOption action = ActionOption.AddNewFish;
            var actionOptionValues = Enum.GetValues(typeof(ActionOption)).Cast<ActionOption>();

            while (parsed == false)
            {
                action = (ActionOption)ConsoleInputMethods.ReadPositiveInteger("Введите номер команды:");

                if (actionOptionValues.Contains(action))
                {
                    parsed = true;
                }
                else
                {
                    ConsoleOutputMethods.Warning("Неверный номер команды.");
                }
            }

            return action;
        }

        private void AddNewFish()
        {
            var creator = new FishCreator();
            var success = _aquarium.TryAddFish(creator.Create());

            if (success)
            {
                _previousCommandStatusBar.Update("Рыбка успешно добавлена.", ConsoleColor.Green);
            }
            else
            {
                _previousCommandStatusBar.Update("При попытке добавить рыбку произошла ошибка.", ConsoleColor.Yellow);
            }
        }

        private void RemoveFish()
        {
            int id = ConsoleInputMethods.ReadPositiveInteger("Введите id рыбки: ");
            var success = _aquarium.TryRemoveFish(id);

            if (success)
            {
                _previousCommandStatusBar.Update("Рыбка успешно удалена.", ConsoleColor.Green);
            }
            else
            {
                _previousCommandStatusBar.Update("При попытке удалить рыбку произошла ошибка.", ConsoleColor.Yellow);
            }
        }
        
        private void SkipDay()
        {
            _currentDay++;
            _previousCommandStatusBar.Text = "Наступил новый день.";
            _previousCommandStatusBar.ForegroundColor = ConsoleColor.Green;
            _aquarium.SpendDay();
        }

        #region Private Classes

        private class AquariumInfoPrinter
        {
            private const string FishFormat = "{0, 8}  {1,8}  {2,8}";

            public void Print(Aquarium aquarium, ConsoleTable table)
            {
                var sortedFish = aquarium.FishList
                    .Where(fish => fish.Dead)
                    .Concat(aquarium.FishList
                    .Where(fish => fish.Dead == false));

                var info = sortedFish.Select(fish => GetFishInfo(fish)).ToList();

                info.Insert(0, new ColoredText(string.Format(FishFormat, "id", "age", "max age")));

                table.Update(info);
            }

            private ColoredText GetFishInfo(Fish fish)
            {
                
                var text = string.Format(FishFormat, fish.Id, fish.CurrentAge, fish.MaxDurationOfLifeInDays);
                var color = fish.Dead ? ConsoleColor.Red : ConsoleColor.Green;

                return new ColoredText(text, color);
            }
        }

        private class Aquarium
        {
            public const int MaxNumberOfFish = 10;

            private readonly List<Fish> _fishList;

            public Aquarium(IEnumerable<Fish> fishContainer)
            {
                _fishList = new List<Fish>();

                var enumerator = fishContainer.GetEnumerator();

                while(Full == false && enumerator.MoveNext())
                {
                    _fishList.Add(enumerator.Current);
                }
            }

            public IReadOnlyList<Fish> FishList => _fishList;

            public bool Full
            {
                get
                {
                    return _fishList.Count >= MaxNumberOfFish;
                }
            }

            public void SpendDay()
            {
                _fishList.ForEach(fish => fish.LiveDay());
            }

            public bool TryAddFish(Fish fish)
            {
                if (Full || fish == null)
                {
                    return false;
                }

                _fishList.Add(fish);

                return true;
            }

            public bool TryRemoveFish(Fish fish)
            {
                return _fishList.Remove(fish);
            }

            public bool TryRemoveFish(int fishId)
            {
                var fishToRemove = _fishList.FirstOrDefault(fish => fish.Id == fishId);

                return _fishList.Remove(fishToRemove);
            }
        }

        private static class UniqueIdGenerator
        {
            private static int _counter = 0;

            /// <summary>
            /// Возвращает новый уникальный Id.
            /// </summary>
            /// <returns>Уникальный Id.</returns>
            /// <exception cref="OverflowException"></exception>
            public static int GetNewId()
            {
                _counter++;

                if (_counter == int.MaxValue)
                {
                    throw new OverflowException("Значение счетчика достигло максимального значения.");
                }

                return _counter;
            }
        }

        private class Fish
        {
            public Fish(int maxAge)
            {
                MaxDurationOfLifeInDays = maxAge;
                CurrentAge = 0;
                Id = UniqueIdGenerator.GetNewId();
            }

            public int Id { get; private set; }

            public int MaxDurationOfLifeInDays { get; private set; }

            public int CurrentAge { get; private set; }

            public bool Dead { get { return CurrentAge >= MaxDurationOfLifeInDays; } }

            public void LiveDay()
            {
                if (Dead == false)
                {
                    CurrentAge++;
                }
            }
        }

        #region Creators

        private interface ICreator<T>
        {
            T Create();
        }

        private class RandomСontainer
        {
            protected readonly Random Rand = new Random();
        }

        private class FishCreator : RandomСontainer, ICreator<Fish>
        {
            const int MinDurationOfLifeInDays = 5;
            const int MaxDurationOfLifeInDays = MinDurationOfLifeInDays * 10;

            public Fish Create()
            {
                return new Fish(Rand.Next(MinDurationOfLifeInDays, MaxDurationOfLifeInDays));
            }
        }

        private class AquariumCreator : RandomСontainer, ICreator<Aquarium>
        {
            const int MinNumberOfFish = 2;
            const int MaxNumberOfFish = Aquarium.MaxNumberOfFish;

            private FishCreator _creator = new FishCreator();

            public Aquarium Create()
            {
                int numberOfFish = Rand.Next(MinNumberOfFish, MaxNumberOfFish + 1);
                List<Fish> fishList = new List<Fish>();

                for (int i =0; i< numberOfFish; i++)
                {
                    fishList.Add(_creator.Create());
                }

                return new Aquarium(fishList);
            }
        }

        #endregion Creators


        #endregion Private Classes
    }
}
