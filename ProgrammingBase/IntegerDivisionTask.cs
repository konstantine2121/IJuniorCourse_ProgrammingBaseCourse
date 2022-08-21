using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;

namespace IJuniorCourse_ProgrammingBaseCourse.ProgrammingBase
{
    /// <summary>
    /// Задача<br/>
    ///<br/>
    ///int a = 10;<br/>
    ///int b = 38;<br/>
    ///int c = (31 – 5 * a) / b;<br/>
    ///Console.WriteLine(c);<br/>
    ///<br/>
    ///ВАЖНО!!! Не запускать код и попытаться подумать головой. Также надо написать ответ “Почему?”
    /// </summary>
    class IntegerDivisionTask : IRunnable
    {
        public void Run()
        {
            int a = 10;
            int b = 38;
            int c = (31 - 5 * a) / b;

            #region Answer

            /*
            5*10 = 50
            31- 50 = -19
            Если бы это была обычная математика, то ответ был бы:
            (-19)  / 38 =  -0,5;
            
            Но при делении int на int остаток от деления отбрасывается/не учитывается.
            ((int) -19)  / (int) 38 = 0.

             */

            #endregion Answer
            Console.WriteLine(GetType().ToString()+":");
            Console.WriteLine(c);

        }
    }
}
