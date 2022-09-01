using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Text;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Создать класс игрока, у которого есть поля с его положением в x,y.<br/>
    ///Создать класс отрисовщик, с методом, который отрисует игрока.<br/>
    ///<br/>
    ///Попрактиковаться в работе со свойствами.
    /// </summary>
    class WorkingWithPropertiesTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            var player = new Player('@', 20, 5);
            Console.WriteLine("Игрок '{0}' находится на позиции ({1}, {2})", player.Marker, player.X, player.Y);

            PlayerRenderer.Draw(player);

            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private class Player
        {
            private const int ConsoleMinLeftOrTopValue = 0;

            private int _x;
            private int _y;

            public Player(char playerMarker, int x,int y)
            {
                Marker = playerMarker;

                X = x;
                Y = y;
            }

            public char Marker{ get; private set; }

            public int X
            {
                get
                {
                    return _x;
                }
                set
                {
                    if (value < ConsoleMinLeftOrTopValue)
                    {
                        _x = 0;
                    }
                    if (value >= Console.BufferWidth)
                    {
                        _x = Console.BufferWidth - 1;
                    }
                    else
                    {
                        _x = value;
                    }
                }
            }

            public int Y
            {
                get
                {
                    return _y;
                }
                set
                {
                    if (value < ConsoleMinLeftOrTopValue)
                    {
                        _y = 0;
                    }
                    if (value >= Console.BufferHeight)
                    {
                        _y = Console.BufferHeight - 1;
                    }
                    else
                    {
                        _y = value;
                    }
                }
            }
        }

        private class PlayerRenderer
        {
            public static void Draw(Player player)
            {
                Console.SetCursorPosition(player.X, player.Y);
                Console.Write(player.Marker);
            }
        }
    }
}
