using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.CommonViews;
using System.Linq;
using System.Text;

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
        private ConsoleTable _aquariumInfoBar;
        private ConsoleRecord _previousCommandStatusBar;
        private ConsoleRecord _commandsInfoBar;
        private int _currentDay = 1;

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();

          
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void Initialize()
        {
            _currentDay = 1;

            int verticalOffset = 12;

            _totalDayPastStatusBar = new ConsoleRecord(0, 0);
            _totalDayPastStatusBar.ForegroundColor = ConsoleColor.Cyan;
            
            _aquariumInfoBar = new ConsoleTable(0, 1);
            
            _previousCommandStatusBar = new ConsoleRecord(0, verticalOffset);
            verticalOffset++;
            
            _commandsInfoBar = new ConsoleRecord(0, verticalOffset);
        }

        private void UpdateInfoBars()
        {
            _totalDayPastStatusBar.Update();
            _aquariumInfoBar.Update();
            _aquariumInfoBar.Update();
            _previousCommandStatusBar.Update();
            _commandsInfoBar.Update();
        }


        #region Private Classes

        private class Aquarium
        {
            const int MaxNumberOfFish = 10;

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

            public bool Full
            {
                get
                {
                    return _fishList.Count >= MaxNumberOfFish;
                }
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

            public void LiveOneDay()
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
            const int MinDurationOfLifeInDays = 200;
            const int MaxDurationOfLifeInDays = MinDurationOfLifeInDays * 10;

            public Fish Create()
            {
                return new Fish(Rand.Next(MinDurationOfLifeInDays,MaxDurationOfLifeInDays));
            }
        }

        #endregion Creators


        #endregion Private Classes
    }
}
