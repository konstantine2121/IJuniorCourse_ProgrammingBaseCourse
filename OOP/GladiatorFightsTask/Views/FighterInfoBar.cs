using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Fighters;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Views;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Views
{
    class FighterInfoBar : ConsoleTable, IFighterInfoBar
    {
        private IFighter _fighter;

        public FighterInfoBar(int positionLeft, int positionTop)
            : base(positionLeft, positionTop)
        {

        }

        public override void Update()
        {
            if (_fighter != null)
            {
                SetNewInfo(_fighter.GetInfo());
            }

            base.Update();
        }

        public void Bind(IFighter fighter)
        {
            _fighter = fighter;
        }
    }
}
