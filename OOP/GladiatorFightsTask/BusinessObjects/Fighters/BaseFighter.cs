using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Fighters;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.BusinessObjects.Fighters
{
    public abstract class BaseFighter : IFighter
    {
        public event Action<IFighter> Died;

        public BaseFighter()
        {
            InitializeStats();
        }

        public string Name { get; protected set; }

        public string Class { get; protected set; }

        public int Health { get; protected set; }

        public bool Dead { get { return Health <= 0; } }

        public int Damage { get; protected set; }

        public virtual int DealDamage()
        {
            return CalculateOutgoingDamage();
        }

        public virtual int TakeDamage(int incomingDamage)
        {
            if (incomingDamage < 0)
            {
                return 0;
            }

            int affectedDamage = CalculateIncomingDamage(incomingDamage);
            Health -= affectedDamage;

            if (Dead)
            {
                Died?.Invoke(this);
            }

            return affectedDamage;
        }

        public virtual void Regenerate()
        {
        }

        public void ResetStats()
        {
            InitializeStats();
        }

        public IReadOnlyList<ColoredText> GetInfo()
        {
            return PrepareInfo();
        }

        protected string FormatLine(string name, object value)
        {
            const string format = "{0, 16}  {1}";
            return string.Format(format, name, value);
        }

        #region Abstract

        protected abstract int CalculateIncomingDamage(int damage);

        protected abstract void InitializeStats();

        protected abstract int CalculateOutgoingDamage();

        protected abstract IReadOnlyList<ColoredText> PrepareInfo();

        #endregion Abstract
    }
}
