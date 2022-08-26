using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.Functions
{
    /// <summary>   
    ///Задача<br/>
    ///<br/>
    ///Сделать игровую карту с помощью двумерного массива. Сделать функцию рисования карты. Помимо этого, дать пользователю возможность перемещаться по карте и взаимодействовать с элементами (например пользователь не может пройти сквозь стену)<br/>
    ///<br/>
    ///Все элементы являются обычными символами<br/>
    /// </summary>
    class GameMapTask : IRunnable
    {
        private const char EmptyField = ' ';
        private const char Wall = '#';
        private const char Player = '@';
        private const char Exit = '$';

        #region Map Content

        private string _mapContent =
" ███████████████████████████...........\n" +
" ██         █              ███        .\n" +
"██    █     █  ████████    ██ ██      .\n" +
"██    ███      █               █      .\n" +
" ██   ██████████    █  █       █      .\n" +
"  ████         █ ███████     ██       .\n" +
"  █              ██    ████   █       .\n" +
" ██ █        ██           █████████   .\n" +
" █ ██        █       ██     █     ██  .\n" +
" █ ██       ██       █     █████   ██ .\n" +
"██  ██████  ██      ██     ██  ██   █ .\n" +
"██ █          ████  ██     █    █   █ .\n" +
"██ ██       ██       ███  ██   ██   █ .\n" +
"██ █       ██      █      █   ██   ██ .\n" +
"██  █████   ███   ██     █████    ██  .\n" +
" █            ██   ██   ██     ███    .\n" +
" █ ██      ██       ██  █  █████      .\n" +
" █  ██     ██    █     ██ ██          .\n" +
"  █  ████   ███  ██       █           .\n" +
"   ████████████████████████           .\n" ;

        #endregion Map Content

        private Point _playerLocation;
        private Point _exitLocation;

        private char[,] _map;

        private ConsoleColor _tempForegroundColor;
        private ConsoleColor _tempBackgroundColor;

        #region IRunnable Implementation

        public void Run()
        {
            ConsoleOutputMethods.Info(
                "Для управления используйте стрелочки.\n" +
                "По достижении выхода игра завершится.\n" +
                "Нажми любую кнопку, чтобы начать.", ConsoleColor.Cyan);
            Console.ReadKey();

            Console.CursorVisible = false;
            _playerLocation = new Point(5, 4);
            _exitLocation = new Point(29, 8);

            InitializeMap();

            bool onExit = false;

            DrawScene();

            while (onExit == false)
            {
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true);
                    var isWall = CheckWall(input.Key, out Point nextPlayerLocation);

                    if (isWall == false)
                    {
                        CleanPlayer();
                        _playerLocation = nextPlayerLocation;
                    }

                    if (_playerLocation == _exitLocation)
                    {
                        onExit = true;
                    }
                    DrawPlayer();
                }
            }

            Console.SetCursorPosition(0, _map.Length + 2);

            ConsoleOutputMethods.Info("Вы добрались до выхода.");
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        #region Drawing

        private void DrawScene()
        {
            Console.Clear();
            DrawMap();
            DrawPlayer();
            DrawExit();
        }

        private void DrawMap()
        {
            if (_map == null) return;

            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    Console.Write(_map[i, j]);
                }
                Console.WriteLine();
            }
        }

        private void DrawExit()
        {
            SaveColors();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(_exitLocation.x, _exitLocation.y);
            Console.Write(Exit);

            RestoreColors();
        }

        private void DrawPlayer()
        {
            SaveColors();

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.SetCursorPosition(_playerLocation.x, _playerLocation.y);
            Console.Write(Player);
            Console.SetCursorPosition(_playerLocation.x, _playerLocation.y);

            RestoreColors();
        }

        private void CleanPlayer()
        {
            Console.SetCursorPosition(_playerLocation.x, _playerLocation.y);
            Console.Write(EmptyField);
        }

        private void SaveColors()
        {
            _tempBackgroundColor = Console.BackgroundColor;
            _tempForegroundColor = Console.ForegroundColor;
        }

        private void RestoreColors()
        {
            Console.BackgroundColor = _tempBackgroundColor;
            Console.ForegroundColor = _tempForegroundColor;
        }

        #endregion Drawing

        private bool CheckWall(ConsoleKey input, out Point nextPlayerLocation)
        {
            bool wall = true;

            Point nextField = _playerLocation;
            nextPlayerLocation = _playerLocation;

            switch (input)
            {
                case ConsoleKey.UpArrow:
                    nextField.y--;
                    break;

                case ConsoleKey.DownArrow:
                    nextField.y++;
                    break;

                case ConsoleKey.LeftArrow:
                    nextField.x--;
                    break;

                case ConsoleKey.RightArrow:
                    nextField.x++;
                    break;
            }

            if (nextField.x < 0 || nextField.y < 0 
                || nextField.y >= _map.GetLength(0)
                || nextField.x >= _map.GetLength(1))
            {
                return true;
            }

            wall = _map[nextField.y, nextField.x] == Wall;

            if (wall == false)
            {
                nextPlayerLocation = nextField;
            }

            return wall;
        }

        #region Map Loading

        private void InitializeMap()
        {
            _map = GetCharMatrixFromString(_mapContent, 40, 20);
        }

        private char [,] GetCharMatrixFromString(string mapContent, int mapWidth,int mapHeight)
        {
            char[,] matrix = new char[mapHeight, mapWidth];

            var lines = mapContent.Split('\n');

            for (int i = 0;i < matrix.GetLength(0); i++)            
            {
                string line = string.Empty;

                if (i < lines.Length)
                {
                    line = lines[i];
                }

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    char charToInsert = EmptyField;

                    if (j < line.Length && line[j] != EmptyField)
                    {
                        charToInsert = Wall;
                    }

                    matrix[i, j] = charToInsert;
                }
            }

            return matrix;
        }

        #endregion Map Loading

        #region Private Structs        

        private struct Point
        {
            public int x;
            public int y;

            public Point(int x,int y)
            {
                this.x = x;
                this.y = y;
            }

            public static bool operator == (Point a, Point b)
            {
                return a.x == b.x && a.y == b.y;
            }

            public static bool operator !=(Point a, Point b)
            {
                return a.x != b.x || a.y != b.y;
            }
        } 

        #endregion Private Classes
    }
}
