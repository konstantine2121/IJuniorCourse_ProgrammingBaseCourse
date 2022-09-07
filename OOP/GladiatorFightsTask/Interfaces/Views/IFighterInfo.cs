using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;
using System.Collections.Generic;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Views
{
    interface IFighterInfo
    {
        void Update(IEnumerable<ColoredText> infoLines);
    }
}
