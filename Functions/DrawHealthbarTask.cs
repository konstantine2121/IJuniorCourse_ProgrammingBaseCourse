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
            HealthBar healthBar = new HealthBar(0,1);
            HealthBar manaBar = new HealthBar(20, 1);

            healthBar.Health = 4;
            manaBar.Health = 8;

            healthBar.ForegroundColor = ConsoleColor.Red;
            manaBar.ForegroundColor = ConsoleColor.Blue;

            healthBar.Update();
            manaBar.Update();

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

        private class HealthBar : ConsoleRecord
        {
            public const char LeftFrame = '[';
            public const char RightFrame = ']';
            
            public const char FilledValue = '#';
            public const char EmptyValue = '_';

            protected int _health;

            public HealthBar(int cursorLeft, int cursorTop) : 
                base(cursorLeft,cursorTop)
            {
                MaxHealth = 10;
                Health = MaxHealth;
            }

            public int MaxHealth { get; set; }

            public int Health
            {
                get
                {
                    return _health;
                }
                set
                {
                    if (value > MaxHealth)
                    {
                        _health = MaxHealth;
                    }
                    else if (value < 0)
                    {
                        _health = 0;
                    }
                    else
                    {
                        _health = value;
                    }
                }
            }

            public override void Update()
            {
                Text = GetHealthBarContent();
                base.Update();
            }

            private string GetHealthBarContent()
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append(LeftFrame);
                stringBuilder.Append(FilledValue, Health); //Это цикл.
                stringBuilder.Append(EmptyValue, MaxHealth -  Health); //И это тоже цикл.
                stringBuilder.Append(RightFrame);

                return stringBuilder.ToString();
            }
        }
    }
}
