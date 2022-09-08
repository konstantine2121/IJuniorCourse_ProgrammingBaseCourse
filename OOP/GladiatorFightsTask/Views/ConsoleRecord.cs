﻿using System;
using IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Views
{
    public class ConsoleRecord
    {
        public ConsoleRecord(int cursorLeft, int cursorTop)
        {
            CursorLeft = cursorLeft;
            CursorTop = cursorTop;
        }

        public string Text { get; set; }

        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;

        public int CursorLeft { get; private set; }

        public int CursorTop { get; private set; }

        public virtual void Update()
        {
            ConsoleColor tempColor = Console.ForegroundColor;
            Console.ForegroundColor = ForegroundColor;

            int positionY = CursorTop;

            var lines = Text.Split(new[] { '\r', '\n' }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                Console.SetCursorPosition(CursorLeft, positionY);
                Console.Write(line);
                positionY++;
            }

            Console.ForegroundColor = tempColor;
        }

        public virtual void Update(ColoredText coloredText)
        {
            ForegroundColor = coloredText.FontColor;
            Text = coloredText.Text;
            Update();
        }
    }
}
