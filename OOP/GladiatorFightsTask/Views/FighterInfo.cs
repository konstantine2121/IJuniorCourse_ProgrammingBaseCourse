using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Views
{
    class FighterInfo : ConsoleTable, IFighterInfo
    {
        public FighterInfo(int positionLeft, int positionTop) 
            : base(positionLeft,positionTop)
        {

        }
    }
}
