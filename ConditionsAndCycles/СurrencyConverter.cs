using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;

namespace IJuniorCourse_ProgrammingBaseCourse.ConditionsAndCycles
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Написать конвертер валют (3 валюты).<br/>
    ///<br/>
    ///У пользователя есть баланс в каждой из представленных валют. Он может попросить сконвертировать часть баланса с одной валюты в другую. Тогда у него с баланса одной валюты снимется X и зачислится на баланс другой Y. Курс конвертации должен быть просто прописан в программе.<br/>
    ///По имени переменной курса конвертации должно быть понятно, из какой валюты в какую валюту конвертируется.<br/>
    ///<br/>
    ///Программа должна завершиться тогда, когда это решит пользователь.
    /// </summary>
    class СurrencyConverter : IRunnable
    {
        private enum Currency
        {
            InvalidValue = -1,
            Euro = 0,
            Dollar = 1,
            Ruble = 2
        }

        private enum ConvertionType
        {
            InvalidOperation = -1,
            EuroToDollar,
            EuroToRubble,
            DollarToEuro,
            DollarToRuble,
            RubleToEuro,
            RubleToDollar,
            SameCurrency
        }

        private double _euroToDollarCost = 1.01;
        private double _euroToRubbleCost = 59;
        private double _rubleToDollarCost = 1 / 56;

        private Dictionary<ConvertionType, double> _dictionaryConversionCosts;

        private bool _exitSignal = false;
        private const string ExitCommand = "exit";

        public void Run()
        {
            _exitSignal = false;

            string QuestionCurrencyToBuy = string.Format(
                "Введите валюту, которую желаете купить ( Евро = {0}, Доллар = {1}, Рубль = {2}).\nДля выхода наберите '{3}'.",
                Currency.Euro, Currency.Dollar, Currency.Ruble, ExitCommand);

            string QuestionCurrencyToSell = string.Format(
                "Введите валюту, которую желаете продать ( Евро = {0}, Доллар = {1}, Рубль = {2}).\nДля выхода наберите '{3}'.",
                Currency.Euro, Currency.Dollar, Currency.Ruble, ExitCommand);

            while (_exitSignal == false)
            {
                var сurrencyToBuy = GetСurrency(QuestionCurrencyToBuy);
                
                if (_exitSignal == true)
                {
                    break;
                }

                var сurrencyToSell = GetСurrency(QuestionCurrencyToSell);

                if (_exitSignal == true)
                {
                    break;
                }

            }

            InitializeConversionCostsContainer();
        }

        private void InitializeConversionCostsContainer()
        {
            //Делаем допущение, что котировки не "плавают" относительно друг-друга, а их обратное преобразование прямо противоположно.
            _dictionaryConversionCosts = new Dictionary<ConvertionType, double>(){
                { ConvertionType.EuroToDollar, _euroToDollarCost},
                { ConvertionType.EuroToRubble, _euroToRubbleCost},
                { ConvertionType.DollarToEuro, 1 / _euroToDollarCost},
                { ConvertionType.DollarToRuble, 1 / _rubleToDollarCost},
                { ConvertionType.RubleToEuro, 1 / _euroToRubbleCost},
                { ConvertionType.RubleToDollar, _rubleToDollarCost},
                { ConvertionType.SameCurrency, 1}
            };
        }

        private Currency GetСurrency(string message)
        {
            var result = Currency.InvalidValue;
            var parsed = false;

            while (_exitSignal == false && parsed == false)
            {
                Console.WriteLine(message);
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Введена пустая строка. Повторите ввод данных.");
                    continue;
                }

                if (input.Equals(ExitCommand))
                {
                    _exitSignal = true;
                    Console.WriteLine("Введена команда выхода.");                    
                    break;
                }

                parsed = Enum.TryParse(input, out result);

                if (parsed == false)
                {
                    Console.WriteLine("Введенная строка имеет неверный формат. Повторите ввод данных.");
                }
            }

            return result;
        }

        private ConvertionType GetConversionType(Currency currencyToSell, Currency currencyToBuy)
        {
            if (currencyToSell == Currency.InvalidValue || currencyToBuy == Currency.InvalidValue)
            {
                return ConvertionType.InvalidOperation;
            }

            if (currencyToSell - currencyToBuy == 0)
            {
                return ConvertionType.SameCurrency;
            }

            switch (currencyToSell)
            {
                case Currency.Euro:
                    if (currencyToBuy == Currency.Dollar)
                    {
                        return ConvertionType.EuroToDollar;
                    }
                    if (currencyToBuy == Currency.Ruble)
                    {
                        return ConvertionType.EuroToRubble;
                    }
                    break;
                case Currency.Dollar:
                    if (currencyToBuy == Currency.Euro)
                    {
                        return ConvertionType.DollarToEuro;
                    }
                    if (currencyToBuy == Currency.Ruble)
                    {
                        return ConvertionType.DollarToRuble;
                    }
                    break;
                case Currency.Ruble:
                    if (currencyToBuy == Currency.Euro)
                    {
                        return ConvertionType.RubleToEuro;
                    }
                    if (currencyToBuy == Currency.Dollar)
                    {
                        return ConvertionType.RubleToDollar;
                    }
                    break;
            }

            return ConvertionType.InvalidOperation;
        }
    }
}
