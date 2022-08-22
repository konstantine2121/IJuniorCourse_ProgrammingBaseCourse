﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse
{
    class ConsoleOutputMethods
    {
        public static void WriteLine(string message, ConsoleColor foregroundColor)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }

        public static void Info(string message, ConsoleColor foregroundColor = ConsoleColor.Green)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }

        public static void Warning(string message, ConsoleColor foregroundColor = ConsoleColor.Yellow)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }
    }
}