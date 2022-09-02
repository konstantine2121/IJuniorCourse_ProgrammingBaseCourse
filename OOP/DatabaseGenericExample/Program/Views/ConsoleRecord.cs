using System;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Program.Views
{
    public class ConsoleRecord
    {
        public event Action<string> TextChanged;

        public ConsoleColor ForegroundColor = ConsoleColor.White;

        private string _text = string.Empty;

        public ConsoleRecord(int cursorLeft, int cursorTop)
        {
            CursorLeft = cursorLeft;
            CursorTop = cursorTop;
        }

        public string Text { 
            get 
            { 
                return _text; 
            } 
            set 
            {
                if (_text.Equals(value) == false)
                {
                    _text = value;

                    TextChanged?.Invoke(_text);
                }
            } 
        }

        public int CursorLeft { get; private set; }

        public int CursorTop { get; private set; }

        public virtual void Update()
        {
            ConsoleColor tempColor = Console.ForegroundColor;
            Console.ForegroundColor = ForegroundColor;

            int positionY = CursorTop;

            var lines = Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var line in lines)
            {
                Console.SetCursorPosition(CursorLeft, positionY);
                Console.Write(line);
                positionY++;
            }

            Console.ForegroundColor = tempColor;
        }
    }
}
