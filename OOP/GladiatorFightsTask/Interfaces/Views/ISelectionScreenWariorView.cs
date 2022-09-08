using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Views
{
    interface ISelectionScreenWariorView
    {
        IFighterInfoBar Fighter1 { get; }

        IFighterInfoBar Fighter2 { get; }

        IFighterInfoBar Fighter3 { get; }

        IFighterInfoBar Fighter4 { get; }

        IFighterInfoBar Fighter5 { get; }

        void Update();
    }
}
