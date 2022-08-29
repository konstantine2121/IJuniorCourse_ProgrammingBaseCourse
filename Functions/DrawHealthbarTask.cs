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
            BaseBar healthBar = new BaseBar(0,1,20);
            BaseBar manaBar = new BaseBar(30, 1,30);

            healthBar.MaxValue = 400;
            healthBar.CurrentValue = 250;

            manaBar.MaxValue = 200;
            manaBar.CurrentValue = 69;
            
            healthBar.ForegroundColor = ConsoleColor.Red;
            manaBar.ForegroundColor = ConsoleColor.Blue;

            healthBar.Update();
            manaBar.Update();

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Здоровье: {0} / {1}", healthBar.CurrentValue, healthBar.MaxValue);
            Console.WriteLine("Мана: {0} / {1}", manaBar.CurrentValue, manaBar.MaxValue);

            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private class ConsoleRecord
        {         
            public ConsoleRecord(int cursorLeft, int cursorTop)
            {
                CursorLeft = cursorLeft;
                CursorTop = cursorTop;

                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;
            }

            public string Text { get; set; }

            public ConsoleColor BackgroundColor { get; set; }

            public ConsoleColor ForegroundColor { get; set; }

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

        private class BaseBar : ConsoleRecord
        {
            public const char LeftFrame = '[';
            public const char RightFrame = ']';            
            public const char FilledValue = '#';
            public const char EmptyValue = '_';

            protected int currentValue;
            protected int maxValue;
            protected int barWidth;

            public BaseBar(int cursorLeft, int cursorTop, int width) : 
                base(cursorLeft,cursorTop)
            {
                MaxValue = 10;
                CurrentValue = MaxValue;
                barWidth = width;
            }

            public int MaxValue
            {
                get
                {
                    return maxValue;
                }
                set
                {
                    if (value < 0)
                    {
                        maxValue = 0;
                    }
                    else
                    {
                        maxValue = value;
                    }

                    if (value < CurrentValue)
                    {
                        CurrentValue = maxValue;
                    }
                }
            }

            public int CurrentValue
            {
                get
                {
                    return currentValue;
                }
                set
                {
                    if (value > MaxValue)
                    {
                        currentValue = MaxValue;
                    }
                    else if (value < 0)
                    {
                        currentValue = 0;
                    }
                    else
                    {
                        currentValue = value;
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

                int filledCells =(int) Math.Round((double)CurrentValue/MaxValue * barWidth );

                stringBuilder.Append(LeftFrame);
                stringBuilder.Append(FilledValue, filledCells); //Это цикл.
                stringBuilder.Append(EmptyValue, barWidth - filledCells); //И это тоже цикл.
                stringBuilder.Append(RightFrame);

                return stringBuilder.ToString();
            }
        }
    }
}
