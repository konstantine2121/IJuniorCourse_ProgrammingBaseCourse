﻿using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.BusinessObjects.Fighters
{
    /// <summary>
    /// Лечит себя после получения урона.
    /// </summary>
    class Healer : BaseFighter
    {
        public Healer(string name) : base()
        {
            Name = name;
        }

        public int HealRate { get; private set; }

        private int MaxHealth { get; set; }

        protected override void InitializeStats()
        {
            Class = "Лекарь";
            Health = 90;
            MaxHealth = Health;
            Damage = 6;
            HealRate = 4;
        }

        public override int TakeDamage(int incomingDamage)
        {
            var damage = base.TakeDamage(incomingDamage);

            if (Dead == false && damage != 0)
            {
                Health += HealRate;

                if (Health > MaxHealth)
                {
                    Health = MaxHealth;
                }
            }

            return damage;
        }

        protected override IReadOnlyList<ColoredText> PrepareInfo()
        {
            List<ColoredText> infos = new List<ColoredText>();

            infos.Add(new ColoredText(FormatLine("Имя:", Name), ConsoleColor.Cyan));
            infos.Add(new ColoredText(FormatLine("Класс:",Class), ConsoleColor.DarkMagenta));
            infos.Add(new ColoredText(FormatLine("Здоровье:", Health), ConsoleColor.Green));
            infos.Add(new ColoredText(FormatLine("Лечение:", HealRate), ConsoleColor.DarkGreen));
            infos.Add(new ColoredText(FormatLine("Урон:", Damage), ConsoleColor.Red));

            return infos;
        }
    }
}
