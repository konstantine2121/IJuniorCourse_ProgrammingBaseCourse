using System.Collections.Generic;
using System.Linq;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Views;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Views
{
    class FightersSelectionView : IFightersSelectionView
    {
        private readonly ConsoleRecord _chooseFighterLabel;
        private readonly ConsoleRecord _chooseFighterIntro;
        private readonly FighterInfoBar[] _fighterInfos;

        public FightersSelectionView()
        {
            _chooseFighterLabel = new ConsoleRecord(0, 0);
            _chooseFighterLabel.ForegroundColor = System.ConsoleColor.Green;
            _chooseFighterLabel.Text = "Выбери двух бойцов для драки:";

            #region FighterInfos

            const int numberOfClasses = 5;
            const int verticalOffset = 7;
            const int horisontalOffset = 50;

            int verticalPosition = 1;

            _fighterInfos = new FighterInfoBar[numberOfClasses];
            
            _fighterInfos[0] = new FighterInfoBar(0, verticalPosition);
            _fighterInfos[1] = new FighterInfoBar(horisontalOffset, verticalPosition);
            verticalPosition += verticalOffset;
            
           _fighterInfos[2] = new FighterInfoBar(0, verticalPosition);
            _fighterInfos[3] = new FighterInfoBar(horisontalOffset, verticalPosition);
            verticalPosition += verticalOffset;

            _fighterInfos[4] = new FighterInfoBar(0, verticalPosition);
            verticalPosition += verticalOffset;

            #endregion FighterInfos

            _chooseFighterIntro = new ConsoleRecord(0, verticalPosition+1);
            _chooseFighterIntro.Text = string.Empty;
        }

        public IReadOnlyList<IFighterInfoBar> FighterInfoBarList 
        {
            get
            {
                return _fighterInfos.ToList();
            }
        }

        public void Update()
        {
            _chooseFighterLabel.Update();

            foreach(var info in FighterInfoBarList)
            {
                info.Update();
            }

            _chooseFighterIntro.Update();
        }
    }
}
