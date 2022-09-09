using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.BusinessObjects.Fighters
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

        public int CriticalHitRate { get; private set; }

        public int CriticalHitDamageMultiplier { get; private set; }

        protected override void InitializeStats()
        {
            Class = "Берсерк";
            Health = 80;
            Damage = 8;
            CriticalHitRate = 3;
            CriticalHitDamageMultiplier = 2;
            _attackCounter = 0;
        }

        protected override int CalculateOutgoingDamage()
        {
            bool dealCriticalDamage = false;
            _attackCounter++;

            if (_attackCounter % CriticalHitRate == 0)
            {
                dealCriticalDamage = true;
                _attackCounter = 0;
            }

            if (dealCriticalDamage)
            {
                return Damage * CriticalHitDamageMultiplier;
            }

            return Damage;
        }

        protected override IReadOnlyList<ColoredText> PrepareInfo()
        {
            List<ColoredText> infos = new List<ColoredText>();

            infos.Add(new ColoredText(FormatLine("Имя:",Name), ConsoleColor.Cyan));
            infos.Add(new ColoredText(FormatLine("Класс:", Class), ConsoleColor.DarkMagenta));
            infos.Add(new ColoredText(FormatLine("Здоровье:", Health), ConsoleColor.Green));
            infos.Add(new ColoredText(FormatLine("Урон:", Damage), ConsoleColor.Red));
            infos.Add(new ColoredText(FormatLine("Крит.частота:", CriticalHitRate), ConsoleColor.DarkRed));
            infos.Add(new ColoredText(FormatLine("Крит.множ-ль:", CriticalHitDamageMultiplier), ConsoleColor.DarkRed));

            return infos;
        }
    }
}
