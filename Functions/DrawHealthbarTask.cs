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
        private const char LeftFrame = '[';
        private const char RightFrame = ']';
        private const char FilledValue = '#';
        private const char EmptyValue = '_';
        
        #region IRunnable Implementation

        public void Run()
        {
            DrawBar(0, 1, 10, 50, ConsoleColor.Green);
            DrawBar(0, 2, 20, 70, ConsoleColor.Blue);
         
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        /// <summary>
        /// Отрисовывает строку вида [####______], в указанной части консоли.
        /// </summary>
        /// <param name="positionX">Координата Х.</param>
        /// <param name="positionY">Координата Y.</param>
        /// <param name="barWidth">Ширина содержимого без учета закрывающих скобок.</param>
        /// <param name="percent">Процент заполнения полоски.</param>
        /// <param name="color">Цвет текста.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void DrawBar(int positionX, int positionY, int barWidth, int percent, ConsoleColor color)
        {
            const int maxPercentValue = 100;
            const int minPercentValue = 0;
            const int minBarWidthValue = 1;

            int frameWidth = string.Concat(LeftFrame,RightFrame).Length;
            int maxBarWidthValue = Console.LargestWindowWidth - frameWidth;

            if (barWidth < minBarWidthValue || barWidth > maxBarWidthValue)
            {
                throw new ArgumentOutOfRangeException(nameof(barWidth));
            }

            if (percent < minPercentValue || percent > maxPercentValue)
            {
                throw new ArgumentOutOfRangeException(nameof(percent));
            }

            ConsoleColor tempForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.SetCursorPosition(positionX, positionY);
            var barContent = GetBarContent(percent, barWidth);
            Console.Write(barContent);

            Console.ForegroundColor = tempForegroundColor;
        }

        private string GetBarContent(int percent, int barWidth)
        {
            int maxPercentValue = 100;
            StringBuilder stringBuilder = new StringBuilder();

            int filledCells = (int)Math.Round((double)percent / maxPercentValue * barWidth);

            stringBuilder.Append(LeftFrame);
            stringBuilder.Append(FilledValue, filledCells); //Это цикл.
            stringBuilder.Append(EmptyValue, barWidth - filledCells); //И это тоже цикл.
            stringBuilder.Append(RightFrame);

            return stringBuilder.ToString();
        }
    }
}
