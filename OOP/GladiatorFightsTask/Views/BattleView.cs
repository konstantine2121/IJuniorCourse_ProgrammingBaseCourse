using System;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Views;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Views
{
    class BattleView : IBattleView
    {
        private readonly ConsoleRecord _battleLabel;        

        public BattleView()
        {
            _battleLabel = new ConsoleRecord(0, 0);
            _battleLabel.ForegroundColor = System.ConsoleColor.Green;
            _battleLabel.Text = "Поединок в самом разгаре";

            Fighter1 = new FighterInfoBar(0, 1);
            Fighter2 = new FighterInfoBar(50, 1);
        }

        public IFighterInfoBar Fighter1 { get; private set; }

        public IFighterInfoBar Fighter2 { get; private set; }

        public void Update()
        {
            _battleLabel.Update();
            Fighter1.Update();
            Fighter2.Update();

            Console.SetCursorPosition(0, 10);
        }
    }
}
