using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.Functions
{
    /// <summary>
    /// ///<br/>
    ///Задача<br/>
    ///<br/>
    ///Разработайте функцию, которая рисует некий бар (Healthbar, Manabar) в определённой позиции. Она также принимает некий закрашенный процент.<br/>
    ///<br/>
    ///При 40% бар выглядит так:<br/>
    ///<br/>
    ///[####______]<br/>
    /// </summary>
    class DrawHealthbarTask : IRunnable
    {
        #region IRunnable Implementation
        
        public void Run()
        {
            var healthBar = DrawBar(0, 1, 75, 200, 20, ConsoleColor.Green);
            var manaBar = DrawBar(30, 1, 250, 400, 40, ConsoleColor.Blue);

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Здоровье: {0} / {1}", healthBar.CurrentValue, healthBar.MaxValue);
            Console.WriteLine("Мана: {0} / {1}", manaBar.CurrentValue, manaBar.MaxValue);

            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private CommonBar DrawBar(int positionX, int positionY, int value, int maxValue, int barWidth, ConsoleColor color)
        {
            CommonBar bar = new CommonBar(positionX, positionY, barWidth, maxValue, foregroundColor: color);
            bar.CurrentValue = value;
            bar.Update();

            return bar;
        }

        private class ConsoleRecord
        {
            public ConsoleRecord(
                int cursorLeft, 
                int cursorTop, 
                ConsoleColor foregroundColor = ConsoleColor.White , 
                ConsoleColor backgroundColor = ConsoleColor.Black )
            {
                CursorLeft = cursorLeft;
                CursorTop = cursorTop;

                ForegroundColor = foregroundColor;
                BackgroundColor = backgroundColor;
            }

            public string Text { get; set; }

            public ConsoleColor BackgroundColor { get; private set; }

            public ConsoleColor ForegroundColor { get; private set; }

            public int CursorLeft { get; private set; }

            public int CursorTop { get; private set; }

            public virtual void Update()
            {
                ConsoleColor tempForegroundColor = Console.ForegroundColor;
                ConsoleColor tempBackgroundColor = Console.BackgroundColor;

                Console.ForegroundColor = ForegroundColor;
                Console.BackgroundColor = BackgroundColor;

                Console.SetCursorPosition(CursorLeft, CursorTop);
                Console.Write(Text);

                Console.ForegroundColor = tempForegroundColor;
                Console.BackgroundColor = tempBackgroundColor;
            }
        }

        private class CommonBar : ConsoleRecord
        {
            public const char LeftFrame = '[';
            public const char RightFrame = ']';            
            public const char FilledValue = '#';
            public const char EmptyValue = '_';

            private int _currentValue;
            private int _barWidth;

            public CommonBar(
                int cursorLeft, 
                int cursorTop, 
                int width=10, 
                int maxValue = 10,
                ConsoleColor foregroundColor = ConsoleColor.White,
                ConsoleColor backgroundColor = ConsoleColor.Black ) 
                : base(
                      cursorLeft, 
                      cursorTop, 
                      foregroundColor, 
                      backgroundColor )
            {
                MaxValue = maxValue;
                CurrentValue = MaxValue;
                _barWidth = width;
            }

            public int MaxValue { get; private set; }

            public int CurrentValue
            {
                get
                {
                    return _currentValue;
                }
                set
                {
                    if (value > MaxValue)
                    {
                        _currentValue = MaxValue;
                    }
                    else if (value < 0)
                    {
                        _currentValue = 0;
                    }
                    else
                    {
                        _currentValue = value;
                    }
                }
            }

            public override void Update()
            {
                Text = GetBarContent();
                base.Update();
            }

            private string GetBarContent()
            {
                StringBuilder stringBuilder = new StringBuilder();

                int filledCells =(int) Math.Round((double)CurrentValue/MaxValue * _barWidth );

                stringBuilder.Append(LeftFrame);
                stringBuilder.Append(FilledValue, filledCells); //Это цикл.
                stringBuilder.Append(EmptyValue, _barWidth - filledCells); //И это тоже цикл.
                stringBuilder.Append(RightFrame);

                return stringBuilder.ToString();
            }
        }
    }
}
