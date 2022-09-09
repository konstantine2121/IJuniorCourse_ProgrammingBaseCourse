using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.BusinessObjects.Fighters
{
    /// <summary>
    /// Имеет 30% шанс увернуться от атаки.
    /// </summary>
    class Thief : BaseFighter
    {
        private readonly Random _random = new Random();

        public Thief(string name) : base()
        {
            Name = name;
        }

        public int ChanceToAvoidAttack { get; private set; }

        protected override void InitializeStats()
        {
            Class = "Вор";
            Health = 60;
            Damage = 7;
            ChanceToAvoidAttack = 30;
        }

        protected override int CalculateIncomingDamage(int damage)
        {
            if (damage < 0)
            {
                return 0;
            }

            int percentages = 100;
            bool avoidAttack = _random.Next(percentages+1) < ChanceToAvoidAttack;

            if (avoidAttack)
            {
                return 0;
            }

            return damage;
        }

        protected override IReadOnlyList<ColoredText> PrepareInfo()
        {
            List<ColoredText> infos = new List<ColoredText>();

            infos.Add(new ColoredText(FormatLine("Имя:", Name), ConsoleColor.Cyan));
            infos.Add(new ColoredText(FormatLine("Класс:", Class), ConsoleColor.DarkMagenta));
            infos.Add(new ColoredText(FormatLine("Здоровье:", Health), ConsoleColor.Green));
            infos.Add(new ColoredText(FormatLine("Уворот %:", ChanceToAvoidAttack), ConsoleColor.DarkGreen));
            infos.Add(new ColoredText(FormatLine("Урон:", Damage), ConsoleColor.Red));

            return infos;
        }
    }
}
