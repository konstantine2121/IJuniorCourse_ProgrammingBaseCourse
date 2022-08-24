using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
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
            
            int borderLength = frameWidth * 2 + spaceWidth * 2 + name.Length;            

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
