using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;
using System;
using System.Collections.Generic;

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

        int TakeDamade(int incomingDamage);

        IReadOnlyList<ColoredText> GetInfo();

        void Regenerate();
    }
}
