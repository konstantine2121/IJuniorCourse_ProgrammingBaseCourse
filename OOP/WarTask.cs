using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.CommonViews;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.BusinessObjects.Fighters;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Enums;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Fighters;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{

    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Есть 2 взвода. 1 взвод страны один, 2 взвод страны два.<br/>
    ///Каждый взвод внутри имеет солдат.<br/>
    ///Нужно написать программу, которая будет моделировать бой этих взводов.<br/>
    ///Каждый боец - это уникальная единица, он может иметь уникальные способности или же уникальные характеристики, такие как повышенная сила.<br/>
    ///Побеждает та страна, во взводе которой остались выжившие бойцы.<br/>
    ///Не важно, какой будет бой, рукопашный, стрелковый.
    /// </summary>
    class WarTask : IRunnable
    {
        private const int SquadSize = 10;
        private const int ThreadSleepInterval = 1000;

        private ConsoleTable _squad1InfoBar;
        private ConsoleTable _squad2InfoBar;
        private SquadInfoPrinter _printer;
        private Squad _squad1;
        private Squad _squad2;

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();

            while (_squad1.AnyAlive && _squad2.AnyAlive)
            {
                UpdateTargetDesignations();
                ExchangeAttacks();
                UpdateSquadsInfo();

                Thread.Sleep(ThreadSleepInterval);
            }

            PrintEndgameInfo();
            Console.WriteLine("Нажмите любую клавишу для выхода из программы.");
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void Initialize()
        {
            var creator = new SquadCreator();

            _squad1 = creator.Create();
            _squad2 = creator.Create();

            int horisontalOffset = 50;

            _squad1InfoBar = new ConsoleTable(0, 1);
            _squad2InfoBar = new ConsoleTable(horisontalOffset, 1);

            _printer = new SquadInfoPrinter();
        }

        private void UpdateTargetDesignations()
        {
            _squad1.UpdateTargetDesignations(_squad2);
            _squad2.UpdateTargetDesignations(_squad1);
        }

        private void ExchangeAttacks()
        {
            _squad1.DealDamage();
            _squad2.DealDamage();
        }

        private void UpdateSquadsInfo()
        {
            Console.Clear();

            _printer.Print(_squad1, _squad1InfoBar);
            _printer.Print(_squad2, _squad2InfoBar);
        }

        private void PrintEndgameInfo()
        {
            Console.WriteLine("\n\n\nБитва окончена!");

            if (_squad1.AnyAlive == false && _squad2.AnyAlive == false)
            {
                ConsoleOutputMethods.Warning("На этом поле боя нету победителей!");
            }
            else if (_squad2.AnyAlive)
            {
                ConsoleOutputMethods.Info("Победа второго отряда!");
            }
            else
            {
                ConsoleOutputMethods.Info("Победа первого отряда!");
            }
        }

        #region Private Classes

        private class SquadInfoPrinter
        {
            private const string Format = "{0, 10} : {1}";

            public void Print(Squad squad, ConsoleTable infoBar)
            {
                var fightersDead = squad.Fighters.Where(fighter => fighter.Dead).ToList();
                var fightersAlive = squad.Fighters.Where(fighter => fighter.Dead == false).ToList();
                var infoLines = new List<ColoredText>();

                Action<IFighter> addValueToInfo = fighter => infoLines.Add(new ColoredText(GetText(fighter), GetColor(fighter)));

                fightersAlive.ForEach(addValueToInfo);
                fightersDead.ForEach(addValueToInfo);

                infoBar.Update(infoLines);
            }

            private ConsoleColor GetColor(IFighter fighter)
            {
                return fighter.Dead ? ConsoleColor.Gray : ConsoleColor.Green;
            }

            private string GetText(IFighter fighter)
            {
                return string.Format(Format, fighter.Class, fighter.Health);
            }
        }

        class Squad : RandomСontainer
        {            
            private readonly List<IFighter> _fighters;
            private readonly Dictionary<IFighter, IFighter> _targetDesignations;

            public Squad(List<IFighter> fighters)
            {
                _fighters = fighters;

                _targetDesignations = new Dictionary<IFighter, IFighter>();

                foreach (var fighter in _fighters)
                {
                    _targetDesignations.Add(fighter, null);
                }
            }

            public IReadOnlyList<IFighter> Fighters { get { return _fighters; } }

            public bool AnyAlive
            {
                get
                {
                    return _fighters.Any(fighter => fighter.Dead == false);
                }
            }

            public void DealDamage()
            {
                foreach (var pair in _targetDesignations)
                {
                    var target = pair.Value;
                    var source = pair.Key;

                    if (target != null && target.Dead == false)
                    {
                        target.TakeDamage(source.DealDamage());
                    }
                }
            }

            public void UpdateTargetDesignations(Squad enemySquad)
            {
                var aliveEnemies = enemySquad.Fighters.Where(enemy => enemy.Dead == false).ToList();

                foreach (var fighter in _fighters)
                {
                    if (fighter.Dead)
                    {
                        _targetDesignations[fighter] = null;
                    }
                    var needNewTarget = NeedNewTarget(fighter);

                    if (needNewTarget)
                    {
                        int index = Rand.Next(aliveEnemies.Count);
                        _targetDesignations[fighter] = aliveEnemies[index];
                    }
                }
            }

            private bool NeedNewTarget(IFighter fighter)
            {
                if (fighter == null || fighter.Dead 
                    || _targetDesignations.ContainsKey(fighter) == false)
                {
                    return false;
                }

                if (_targetDesignations[fighter] == null)
                {
                    return true;
                }

                return _targetDesignations[fighter].Dead;
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

        private class SquadCreator : RandomСontainer, ICreator<Squad>
        {
            private FighterCreater _creator = new FighterCreater();

            public Squad Create()
            {
                List<IFighter> fighters = new List<IFighter>();

                for (int i = 0;i < SquadSize; i++)
                {
                    fighters.Add(_creator.Create());
                }

                return new Squad(fighters);
            }
        }

        private class FighterCreater : RandomСontainer , ICreator<IFighter>
        {
            public IFighter Create()
            {
                IFighter result = null;

                var name = "Noname";
                var types = Enum.GetValues(typeof(FighterType)).Cast<FighterType>().ToArray();
                var index = Rand.Next(types.Length);

                switch (types[index])
                {
                    case FighterType.Warrior:
                        result = new Warrior(name);
                        break;

                    case FighterType.Healer:
                        result = new Healer(name);
                        break;

                    case FighterType.Berserk:
                        result = new Berserk(name);
                        break;

                    case FighterType.Thief:
                        result = new Thief(name);
                        break;

                    case FighterType.MagicianOfFire:
                        result = new MagicianOfFire(name);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }

                return result;
            }
        }

        #endregion Creators

        #endregion Private Classes
    }
}
