using System;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.GladiatorFightsTask.Dto
{
    public class ColoredText
    {
        public ConsoleColor FontColor;
        public string Text;

        public ColoredText(string text = "", ConsoleColor fontColor = ConsoleColor.White)
        {
            Text = text;
            FontColor = fontColor;
        }
    }
}
