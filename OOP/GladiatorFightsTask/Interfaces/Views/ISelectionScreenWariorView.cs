using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Views
{
    interface ISelectionScreenWariorView
    {
        IFighterInfo Fighter1 { get; }

        IFighterInfo Fighter2 { get; }

        IFighterInfo Fighter3 { get; }

        IFighterInfo Fighter4 { get; }

        IFighterInfo Fighter5 { get; }

        void Update();
    }
}
