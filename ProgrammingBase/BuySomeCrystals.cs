using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;

namespace IJuniorCourse_ProgrammingBaseCourse.ProgrammingBase
{
    /// <summary>
    ///Легенда:<br/>
    ///Вы приходите в магазин и хотите купить за своё золото кристаллы. В вашем кошельке есть какое-то количество золота, продавец спрашивает у вас, сколько кристаллов вы хотите купить? После сделки у вас остаётся какое-то количество золота и появляется какое-то количество кристаллов.<br/>
    ///<br/>
    ///Формально:<br/>
    ///<br/>
    ///При старте программы пользователь вводит начальное количество золота. Потом ему предлагается купить какое-то количество кристаллов по цене N(задать в программе самому). Пользователь вводит число и его золото конвертируется в кристаллы. Остаток золота и кристаллов выводится на экран.<br/>
    ///<br/>
    ///Проверять на то, что у игрока достаточно денег ненужно. 
    /// </summary>
    class BuySomeCrystals : IRunnable
    {
        public void Run()
        {
            const string GoldQuestion = "Введите начальное количество золота: ";
            const string CrystalCostQuestion = "Введите стоимость одного кристала: ";
            const string CrystalsToBuyQuestion = "Сколько кристалов вы желаете купить?: ";

            int gold = 0;
            int crystalCost = 0;
            int crystalsToBuy = 0;
            int crystalsInBag = 0;

            gold = ReadIntValue(GoldQuestion);
            crystalCost = ReadIntValue(CrystalCostQuestion);
            crystalsToBuy = ReadIntValue(CrystalsToBuyQuestion);

            //Так как проверка на достаточное кол-во денег отсутствует, то
            gold -= crystalsToBuy * crystalCost;
            crystalsInBag = crystalsToBuy;

            Console.WriteLine();
            Console.WriteLine($"Остаток по золоту: {gold}");
            Console.WriteLine($"Куплено кристалов: {crystalsInBag}");

        }

        private int ReadIntValue(string message)
        {
            int result = 0;
            bool parsed = false;

            while (parsed == false)
            {
                Console.Write(message);
                var input = Console.ReadLine();
                parsed = int.TryParse(input,out result);

                if (parsed == false)
                {
                    Console.WriteLine("Не получилось распознать значение. Попробуйте еще раз.");                 
                }
                else
                {
                    if (result < 0)
                    {
                        Console.WriteLine("Значение не может быть отрицательным. Попробуйте еще раз.");                        
                    }
                }
            }

            return result;
        }
    }
}
