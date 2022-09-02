using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Views;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Interfaces
{
    public interface IPlayersModuleView
    {
        ConsoleRecord TotalRecords { get; }

        ConsoleRecord ListOfBanned { get; }

        ConsoleRecord DatabaseContent { get; }

        ConsoleRecord InputField { get; }

        ConsoleRecord CommandResult { get; }
        ConsoleRecord CommandsList { get; }

        void Update();
    }
}
