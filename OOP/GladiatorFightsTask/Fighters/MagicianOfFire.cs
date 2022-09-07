using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Fighters
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

        public int ManaSpentOnAttack { get; private set; }

        protected override void InitializeStats()
        {
            Class = "Маг огня";
            Health = 75;
            Damage = 20;
            Mana = 100;
            MaxMana = Mana;
            ManaSpentOnAttack = 40;
            ManaRegenerationRate = 30;
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
            if ((Mana - ManaSpentOnAttack) >= 0 )
            {
                Mana -= ManaSpentOnAttack;

                return Damage;
            }

            return 0;
        }

        protected override IReadOnlyList<ColoredText> PrepareInfo()
        {
            List<ColoredText> infos = new List<ColoredText>();

            infos.Add(new ColoredText("Имя: " + Name, ConsoleColor.Cyan));
            infos.Add(new ColoredText("Класс: " + Class, ConsoleColor.DarkMagenta));
            infos.Add(new ColoredText("Здоровье: " + Health, ConsoleColor.Green));
            infos.Add(new ColoredText("Мана: " + Mana, ConsoleColor.Blue));
            infos.Add(new ColoredText("Кол-во маны на атаку: " + ManaSpentOnAttack, ConsoleColor.DarkBlue));
            infos.Add(new ColoredText("Урон от огня: " + Damage, ConsoleColor.Red));

            return infos;
        }

        public override void Regenerate()
        {
            Mana += ManaRegenerationRate;

            if (Mana > MaxMana)
            {
                Mana = MaxMana;
            }
        }
    }
}
