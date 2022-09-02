using IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Interfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Dto
{
    public class Player : IIdContainer, IClonable<Player>
    {
        public Player(string id, string name, int level, bool banned = false)
        {
            Id = id;
            Name = name;
            Level = level;
            Banned = banned;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public bool Banned { get; set; }

        public Player Clone()
        {
            return new Player(Id, Name, Level, Banned);
        }
    }
}
