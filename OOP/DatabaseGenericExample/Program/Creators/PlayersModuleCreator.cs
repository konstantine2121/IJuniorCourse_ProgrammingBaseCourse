using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Database;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.BusinessComponents;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Controllers;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.DataAccess;
using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Creators
{
    class PlayersModuleCreator
    {
        public PlayersModule Create()
        {
            var database = new PlayersDatabase();
            var daPlayersModule = new DAPlayersModule(database);
            var bCPlayersModule = new BCPlayersModule(daPlayersModule);
            var view = new PlayersModuleView();
            var controller = new PlayersModuleController(bCPlayersModule, view);

            var module = new PlayersModule(bCPlayersModule, view, controller);
            return module;
        }
    }
}
