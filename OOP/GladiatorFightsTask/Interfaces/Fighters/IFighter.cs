using System;
using System.Collections.Generic;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Interfaces.Fighters
{
    public interface IFighter
    {
        event Action<IFighter> Died;

        string Name { get; }

        string Class { get; }

        int Health { get; }

        bool Dead { get; }

        int Damage { get; }

        int DealDamage();

        int TakeDamage(int incomingDamage);

        IReadOnlyList<ColoredText> GetInfo();

        void Regenerate();

        void ResetStats();
    }
}
