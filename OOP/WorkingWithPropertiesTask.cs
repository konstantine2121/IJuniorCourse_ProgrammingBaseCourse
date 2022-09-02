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
            Console.WriteLine("Игрок '{0}' находится на позиции ({1}, {2})", player.Marker, player.LocationX, player.LocationY);

            PlayerRenderer.Draw(player);

            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private class Player
        {
            private const int ConsoleMinLeftOrTopValue = 0;

            private int _locationX;
            private int _locationY;

            public Player(char playerMarker, int locationX,int locationY)
            {
                Marker = playerMarker;

                LocationX = locationX;
                LocationY = locationY;
            }

            public char Marker{ get; private set; }

            public int LocationX
            {
                get
                {
                    return _locationX;
                }
                set
                {
                    if (value < ConsoleMinLeftOrTopValue)
                    {
                        _locationX = 0;
                    }
                    if (value >= Console.BufferWidth)
                    {
                        _locationX = Console.BufferWidth - 1;
                    }
                    else
                    {
                        _locationX = value;
                    }
                }
            }

            public int LocationY
            {
                get
                {
                    return _locationY;
                }
                set
                {
                    if (value < ConsoleMinLeftOrTopValue)
                    {
                        _locationY = 0;
                    }
                    if (value >= Console.BufferHeight)
                    {
                        _locationY = Console.BufferHeight - 1;
                    }
                    else
                    {
                        _locationY = value;
                    }
                }
            }
        }

        private class PlayerRenderer
        {
            public static void Draw(Player player)
            {
                Console.SetCursorPosition(player.LocationX, player.LocationY);
                Console.Write(player.Marker);
            }
        }
    }
}
