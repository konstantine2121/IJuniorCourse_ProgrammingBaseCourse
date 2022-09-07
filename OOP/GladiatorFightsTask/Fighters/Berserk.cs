using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Fighters
{
    /// <summary>
    /// Каждый 3 удар наносит удвоенный урон.
    /// </summary>
    class Berserk : BaseFighter
    {
        private int _attackCounter = 0;

        public Berserk(string name) : base()
        {
            Name = name;
        }

        public int DoubleDamageRate { get; private set; }

        public int DoubleDamageMultiplier { get; private set; }

        protected override void InitializeStats()
        {
            Class = "Берсерк";
            Health = 80;
            Damage = 8;
            DoubleDamageRate = 3;
            DoubleDamageMultiplier = 2;
        }

        protected override int CalculateIncomingDamage(int damage)
        {
            if (damage < 0)
            {
                return 0;
            }

            return damage;
        }

        protected override int CalculateOutgoingDamage()
        {
            bool dealDoubleDamage = false;
            _attackCounter++;

            if (_attackCounter % DoubleDamageRate == 0)
            {
                dealDoubleDamage = true;
            }

            if (_attackCounter >= 3)
            {
                _attackCounter = 0;
            }

            if (dealDoubleDamage)
            {
                return Damage * DoubleDamageMultiplier;
            }

            return Damage;
        }

        protected override IReadOnlyList<ColoredText> PrepareInfo()
        {
            List<ColoredText> infos = new List<ColoredText>();

            infos.Add(new ColoredText("Имя: " + Name, ConsoleColor.Cyan));
            infos.Add(new ColoredText("Класс: " + Class, ConsoleColor.DarkMagenta));
            infos.Add(new ColoredText("Здоровье: " + Health, ConsoleColor.Green));
            infos.Add(new ColoredText("Урон: " + Damage, ConsoleColor.Red));
            infos.Add(new ColoredText("Крит.частота: " + DoubleDamageRate, ConsoleColor.DarkRed));
            infos.Add(new ColoredText("Крит.множитель: " + DoubleDamageMultiplier, ConsoleColor.DarkRed));

            return infos;
        }
    }
}
