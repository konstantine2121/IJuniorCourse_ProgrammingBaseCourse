using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.BusinessObjects.Fighters
{
    /// <summary>
    /// Cнижение получаемого урона за счет брони.
    /// </summary>
    class Warrior : BaseFighter
    {
        public Warrior(string name): base()
        {
            Name = name;
        }

        public int Armor { get; private set; }

        protected override void InitializeStats()
        {
            Class = "Воин";
            Health = 100;
            Damage = 8;
            Armor = 2;
        }

        protected override int CalculateIncomingDamage(int damage)
        {
            if (damage < Armor)
            {
                return 0;
            }

            return damage - Armor;
        }

        protected override IReadOnlyList<ColoredText> PrepareInfo()
        {
            List<ColoredText> infos = new List<ColoredText>();

            infos.Add(new ColoredText(FormatLine("Имя:", Name), ConsoleColor.Cyan));
            infos.Add(new ColoredText(FormatLine("Класс:", Class), ConsoleColor.DarkMagenta));
            infos.Add(new ColoredText(FormatLine("Здоровье:", Health), ConsoleColor.Green));
            infos.Add(new ColoredText(FormatLine("Броня:", Armor), ConsoleColor.DarkGreen));
            infos.Add(new ColoredText(FormatLine("Урон:", Damage), ConsoleColor.Red));

            return infos;
        }
    }
}
