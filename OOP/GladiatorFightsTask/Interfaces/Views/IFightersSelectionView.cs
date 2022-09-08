using System.Collections.Generic;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Views
{
    interface IFightersSelectionView
    {
        IReadOnlyList <IFighterInfoBar> FighterInfoBarList { get; }

        void Update();
    }
}
