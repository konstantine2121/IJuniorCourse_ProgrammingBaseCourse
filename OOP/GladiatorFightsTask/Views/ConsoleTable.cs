using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;
using System;
using System.Collections.Generic;


namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Views
{
    class ConsoleTable
    {
        private readonly List<ColoredText> _rows = new List<ColoredText>();

        public ConsoleTable(int cursorLeft, int cursorTop)
        {
            CursorLeft = cursorLeft;
            CursorTop = cursorTop;
        }

        public int CursorLeft { get; private set; }

        public int CursorTop { get; private set; }

        public virtual void Update()
        {
            ConsoleColor tempColor = Console.ForegroundColor;
            int positionY = CursorTop;

            foreach (var line in _rows)
            {
                Console.ForegroundColor = line.FontColor;
                Console.SetCursorPosition(CursorLeft, positionY);
                Console.Write(line);
                positionY++;
            }

            Console.ForegroundColor = tempColor;
        }

        public virtual void Update(IEnumerable<ColoredText> coloredTexts)
        {
            SetNewInfo(coloredTexts);
            Update();
        }

        public virtual void SetNewInfo(IEnumerable<ColoredText> coloredTexts)
        {
            _rows.Clear();
            _rows.AddRange(coloredTexts);
        }
    }
}
