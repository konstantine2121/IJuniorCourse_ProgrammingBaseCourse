namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Views
{
    interface IBattleView
    {
        IFighterInfoBar Fighter1 { get; }

        IFighterInfoBar Fighter2 { get; }

        void Update();
    }
}
