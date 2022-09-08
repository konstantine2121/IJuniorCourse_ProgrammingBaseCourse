using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Enums;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Fighters;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Views;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Controllers
{
    class ArenaModuleController
    {
        private readonly ArenaModuleLoader _loader;
        private IBattleView _battleView;
        private IFightersSelectionView _selectionView;
        
        private enum ActionOption
        {
            CreateNewFighters = 1,
            RestartBattle,
            ExitGame
        }

        public ArenaModuleController(ArenaModuleLoader loader)
        {
            _loader = loader;
        }

        public void RegisterViews(IBattleView battleView, IFightersSelectionView selectionView)
        {
            _battleView = battleView;
            _selectionView = selectionView;
        }

        public void RunGameCycle()
        {
            bool isPlaying = true;
            bool selectNewFighters = true;

            while(isPlaying)
            {
                if (selectNewFighters)
                {
                    SelectFighters();
                }

                PerformBattle();

                var option = ReadActionOption();

                if (option == ActionOption.ExitGame)
                {
                    isPlaying = false;
                }

                selectNewFighters = option == ActionOption.CreateNewFighters;
            }

            Console.WriteLine("Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }

        #region Select Fighters
        
        private void SelectFighters()
        {
            Console.Clear();

            var fightersToSelectDictionary = _loader.CreateExampleFigters();
            var fightersTypes = fightersToSelectDictionary.Keys.ToList();

            BindSelectionView(fightersToSelectDictionary.Values.ToList());
            _selectionView.Update();

            Console.WriteLine("Перечень классов "+ GetTypesOfFightersString(fightersToSelectDictionary));
            Console.WriteLine();

            ConsoleOutputMethods.Info("Выбираем  первого бойца");

            FighterType fighter1Type = SelectFighterType(fightersTypes);
            string fighter1Name = ConsoleInputMethods.ReadString("Введите имя бойца: ");

            ConsoleOutputMethods.Info("Выбираем  второго бойца");

            FighterType fighter2Type = SelectFighterType(fightersTypes);
            string fighter2Name = ConsoleInputMethods.ReadString("Введите имя бойца: ");

            _loader.CreateFigter1(fighter1Name, fighter1Type);
            _loader.CreateFigter2(fighter2Name, fighter2Type);
        }

        private void BindSelectionView(IReadOnlyList<IFighter> fightersSelectionList)
        {
            var infoBarList = _selectionView.FighterInfoBarList;

            for (int i = 0; i < fightersSelectionList.Count && i < infoBarList.Count; i++)
            {
                infoBarList[i].Bind(fightersSelectionList[i]);
            }
        }

        private FighterType SelectFighterType(IEnumerable<FighterType> fighterTypes)
        {
            FighterType result = FighterType.Warrior;
            bool correct = false;

            while(correct == false)
            {
                result = (FighterType) ConsoleInputMethods.ReadPositiveInteger("Введите тип бойца: ");

                if (fighterTypes.Contains(result))
                {
                    correct = true;
                }
                else
                {
                    ConsoleOutputMethods.Warning("Неверное значение.");
                }
            }

            return result;
        }

        private string GetTypesOfFightersString(Dictionary<FighterType, IFighter> selectionDictionary)
        {
            const string separator = ", ";
            StringBuilder builder = new StringBuilder();

            foreach (var pair in selectionDictionary)
            {
                builder.Append((int)pair.Key + " - " + pair.Value.Class + separator);
            }

            if (builder.Length > separator.Length)
            {
                builder.Remove(builder.Length - separator.Length, separator.Length);
            }
            
            return builder.ToString();
        }

        #endregion Select Fighters

        private void PerformBattle()
        {
            Console.Clear();
            int onePunchInterval = 1000;

            var fighter1 = _loader.Fighter1;
            var fighter2 = _loader.Fighter2;

            fighter1.ResetStats();
            fighter2.ResetStats();

            _battleView.Fighter1.Bind(fighter1);
            _battleView.Fighter2.Bind(fighter2);
            _battleView.Update();

            while (fighter1.Dead == false && fighter2.Dead== false)
            {
                Thread.Sleep(onePunchInterval);

                fighter2.TakeDamage(fighter1.DealDamage());
                
                fighter1.TakeDamage(fighter2.DealDamage());

                fighter1.Regenerate();
                fighter2.Regenerate();

                Console.Clear();
                _battleView.Update();
            }

            PrintEndgameInfo();
        }

        private void PrintEndgameInfo()
        {
            Console.WriteLine("\nБитва окончена!");

            if (_loader.Fighter1.Dead && _loader.Fighter2.Dead)
            {
                ConsoleOutputMethods.Warning("На этом поле боя нету победителей!");
            }
            else if (_loader.Fighter1.Dead)
            {
                ConsoleOutputMethods.Info("Победа второго бойца!");
            }
            else
            {
                ConsoleOutputMethods.Info("Победа первого бойца!");                
            }
        }

        private ActionOption ReadActionOption()
        {
            Console.WriteLine();

            string commandsInfo = "Выбрать новых бойцов - "+(int)ActionOption.CreateNewFighters +
                "\nПерезапустить бой - "+ (int)ActionOption.RestartBattle +
                "\nУйти из колизея - "+ (int)ActionOption.ExitGame;

            ConsoleOutputMethods.Info("Что вы желаете сделать?: ");
            Console.WriteLine(commandsInfo);

            bool parsed = false;
            ActionOption action = ActionOption.CreateNewFighters;
            var actionOptionValues = Enum.GetValues(typeof(ActionOption)).Cast<ActionOption>();

            while (parsed == false)
            {
                action = (ActionOption) ConsoleInputMethods.ReadPositiveInteger("Введите номер команды:");

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
    }
}
