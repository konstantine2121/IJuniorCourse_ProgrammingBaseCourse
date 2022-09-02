using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.BusinessComponents;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Interfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Controllers
{
    class PlayersModule : IRunnable
    {
        public PlayersModule(BCPlayersModule model, IPlayersModuleView view, PlayersModuleController controller)
        {
            Model = model;
            View = view;
            Controller = controller;
        }

        protected BCPlayersModule Model {get; private set;}

        protected IPlayersModuleView View {get; private set;}

        protected PlayersModuleController Controller {get; private set;}

        #region IRunnable Implementation

        public void Run()
        {
            Controller.RunMainCycle();
        }

        #endregion IRunnable Implementation
    }
}
