using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Controllers
{
    class ArenaModuleController
    {
        private readonly ArenaModuleLoader _loader;
        private readonly IArenaModuleView _view;

        public ArenaModuleController(ArenaModuleLoader loader, IArenaModuleView view)
        {
            _loader = loader;
            _view = view;
        }

    }
}
