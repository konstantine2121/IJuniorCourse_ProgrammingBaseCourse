using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IJuniorCourse_ProgrammingBaseCourse.Collections
{
    /// <summary>
    /// ///Задача<br/>
    ///<br/>
    ///Создать программу, которая принимает от пользователя слово и выводит его значение. <br/>
    ///Если такого слова нет, то следует вывести соответствующее сообщение.<br/>
    /// </summary>
    class ExplanatoryDictionaryTask : IRunnable
    {
        Dictionary<string, string> _dictionary;

        #region IRunnable Implementation

        public void Run()
        {            
            const string exitCommand = "!q";
            const string listCommand = "!all";

            string info = $"Введите слово ('{exitCommand}' - выход, '{listCommand}' - вывести список слов): ";

            bool working = true;

            InitDictionary();

            while (working)
            {
                var input = ConsoleInputMethods.ReadString(info);

                switch(input)
                {
                    case exitCommand:
                        working = false;
                        break;

                    case listCommand:
                        PrintKeys();
                        break;

                    default:
                        PrintWordExplanatory(input);
                        break;                        
                }
            }

            Console.WriteLine("Выход из программы.");
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void PrintWordExplanatory(string word)
        {
            if (_dictionary.ContainsKey(word.ToLower()))
            {
                string formatedWord = word.FirstOrDefault().ToString().ToUpper() + word.Substring(1);

                ConsoleOutputMethods.Info($"Значение слова '{formatedWord}':");
                Console.WriteLine(formatedWord + " - " + _dictionary[word]);
            }
            else
            {
                ConsoleOutputMethods.Warning($"Значение слова '{word}' не найдено!");
            }
        }

        private void InitDictionary()
        {
            #region rawFileContent

            //Девизы Империума Человечества Warhammer 40k.
            string rawFileContent = @"
Безделье — суть ереси.
Безжалостность — доброта мудрого.
Боль — иллюзия чувств.
Страх - отрицание веры.
Отчаяние — иллюзия разума.
Великодушие — признак слабости.
Жизнь — это валюта Императора, распоряжайся ею разумно.
Смерть — освобождение.
Император — это все.
Незнание — добродетель.
Молчание — знак согласия.
Знание — сила, скрой его.
Надежда — первый шаг на пути к разочарованию.
Ненависть — наибольший дар Императора человечеству.
Невежество — вот отличие еретиков от предателей.
Оправдания — удел слабых.
Прощение — признак слабости.
Счастье — самообман слабых.
Вера — мой щит! 
Ярость — мой меч!";

            #endregion rawFileContent

            _dictionary = new Dictionary<string, string>();

            var lines = rawFileContent.Split(new char[] { '\n','\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(new char[] { '-', '—' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    var word = parts[0].Trim().ToLower();
                    var explanatory = parts[1].Trim();

                    if (_dictionary.ContainsKey(word) == false)
                    {
                        _dictionary.Add(word, explanatory);
                    }
                }
            }
        }

        private void PrintKeys()
        {
            ConsoleOutputMethods.Info("Список доступных слов:");

            foreach (var key in _dictionary.Keys)
            {
                Console.WriteLine(key);
            }

            Console.WriteLine();
        }
    }
}
