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
    ///Создайте переменную типа string, в которой хранится пароль для доступа к тайному сообщению. Пользователь вводит пароль, далее происходит проверка пароля на правильность, и если пароль неверный, то попросите его ввести пароль ещё раз. Если пароль подошёл, выведите секретное сообщение.<br/>
    ///<br/>
    ///Если пользователь неверно ввел пароль 3 раза, программа завершается.
    /// </summary>
    class PasswordCheckTask :IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            string password = "123";
            int numberOfTries = 3;
            bool autorisationPassed = false;

            string secretText = "Какой то секретный текст.";

            for (int i =0; i < numberOfTries && autorisationPassed == false; i++)
            {
                var input = ConsoleInputMethods.ReadString("Введите пароль: ");
                
                if (input.Equals(password))
                {
                    autorisationPassed = true;
                    ConsoleOutputMethods.Info("Вывод секретной информации.");
                    Console.WriteLine(secretText);
                }
                else
                {
                    ConsoleOutputMethods.Warning("Неверный пароль. Попробуйте еще раз.");
                }
            }

            if (autorisationPassed == false)
            {
                ConsoleOutputMethods.Warning("Количество попыток исчерпано. Доступ запрещен!");
            }

            Console.WriteLine();
            Console.WriteLine("Выход из приложения");
            Console.ReadLine();
        }

        #endregion IRunnable Implementation

    }
}
