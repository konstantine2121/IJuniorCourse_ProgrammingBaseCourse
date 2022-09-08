using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.BusinessObjects.Fighters
{
    class MagicianOfFire : BaseFighter
    {
        public MagicianOfFire(string name) : base()
        {
            Name = name;
        }

        public int Mana { get; private set; }

        public int MaxMana { get; private set; }

        public int ManaRegenerationRate { get; private set; }

        public int AttackManaCost { get; private set; }

        protected override void InitializeStats()
        {
            Class = "Маг огня";
            Health = 75;
            Damage = 25;
            Mana = 100;
            MaxMana = Mana;
            AttackManaCost = 40;
            ManaRegenerationRate = 15;
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
            if ((Mana - AttackManaCost) >= 0 )
            {
                Mana -= AttackManaCost;

                return Damage;
            }

            return 0;
        }

        protected override IReadOnlyList<ColoredText> PrepareInfo()
        {
            List<ColoredText> infos = new List<ColoredText>();

            infos.Add(new ColoredText(FormatLine("Имя:", Name), ConsoleColor.Cyan));
            infos.Add(new ColoredText(FormatLine("Класс:", Class), ConsoleColor.DarkMagenta));
            infos.Add(new ColoredText(FormatLine("Здоровье:", Health), ConsoleColor.Green));
            infos.Add(new ColoredText(FormatLine("Мана:", Mana), ConsoleColor.Blue));
            infos.Add(new ColoredText(FormatLine("Реген.маны:", ManaRegenerationRate), ConsoleColor.Blue));
            infos.Add(new ColoredText(FormatLine("Маны на каст:", AttackManaCost), ConsoleColor.DarkBlue));
            infos.Add(new ColoredText(FormatLine("Урон от огня:", Damage), ConsoleColor.Red));

            return infos;
        }

        public override void Regenerate()
        {
            if (Dead)
            {
                return;
            }

            Mana += ManaRegenerationRate;

            if (Mana > MaxMana)
            {
                Mana = MaxMana;
            }
        }
    }
}
