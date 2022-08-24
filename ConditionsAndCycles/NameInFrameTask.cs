using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Вывести имя в прямоугольник из символа, который введет сам пользователь.<br/>
    ///<br/>
    ///Вы запрашиваете имя, после запрашиваете символ, а после отрисовываете в консоль его имя в прямоугольнике из его символов.<br/>
    ///<br/>
    ///Пример:<br/>
    ///<br/>
    ///Alexey<br/>
    ///<br/>
    ///%<br/>
    ///<br/>
    ///%%%%%%<br/>
    ///% Alexey %<br/>
    ///%%%%%%<br/>
    ///<br/>
    ///Примечание:<br/>
    ///<br/>
    ///Длину строки можно всегда узнать через свойство Length<br/>
    ///<br/>
    ///string someString = “Hello”;<br/>
    ///<br/>
    ///Console.WriteLine(someString.Length); //5<br/>
    /// </summary>
    class NameInFrameTask :IRunnable
    {

        #region IRunnable Implementation

        public void Run()
        {
            char frameChar = '#';
            int frameWidth = 1;

            string name = string.Empty;

            name = ConsoleInputMethods.ReadString("Введите имя: ");
            var charInput = ConsoleInputMethods.ReadString("Введите символ: ");

            frameChar = charInput[0];

            char spaceChar = ' ';
            int spaceWidth = 1;

            int numberOfVerticalFrameSections = 2;
            
            int borderLength = frameWidth * numberOfVerticalFrameSections + spaceWidth * numberOfVerticalFrameSections + name.Length;            

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(frameChar, borderLength);
            stringBuilder.AppendLine();

            stringBuilder.Append(frameChar, frameWidth);
            stringBuilder.Append(spaceChar, spaceWidth);
            stringBuilder.Append(name);
            stringBuilder.Append(spaceChar, spaceWidth);
            stringBuilder.Append(frameChar, frameWidth);
            stringBuilder.AppendLine();

            stringBuilder.Append(frameChar, borderLength);
            stringBuilder.AppendLine();

            ConsoleOutputMethods.Info("Результат:");
            Console.WriteLine(stringBuilder.ToString());
        }

        #endregion IRunnable Implementation
    }
}
