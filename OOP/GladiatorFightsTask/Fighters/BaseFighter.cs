using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Fighters;
using System;
using System.Collections.Generic;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Fighters
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

        public virtual int TakeDamade(int incomingDamage)
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

        public IReadOnlyList<ColoredText> GetInfo()
        {
            return PrepareInfo();
        }

        #region Abstract

        protected abstract int CalculateIncomingDamage(int damage);

        protected abstract void InitializeStats();

        protected abstract int CalculateOutgoingDamage();

        protected abstract IReadOnlyList<ColoredText> PrepareInfo();

        #endregion Abstract
    }
}
