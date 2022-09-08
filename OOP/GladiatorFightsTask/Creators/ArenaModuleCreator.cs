using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Controllers;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Views;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Creators
{
    class ArenaModuleCreator
    {
        public ArenaModule Create()
        {
            var loader = new ArenaModuleLoader();
            var controller = new ArenaModuleController(loader);
            var battleView = new BattleView();
            var selectionView = new FightersSelectionView();
            controller.RegisterViews(battleView, selectionView);
            var arenaModule = new ArenaModule(controller);

            return arenaModule;
        }
    }
}
