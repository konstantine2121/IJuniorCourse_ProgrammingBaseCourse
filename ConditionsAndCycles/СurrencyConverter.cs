using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
        private const string ExitCommand = "exit";

        private double _euroToDollarRate = 1.1;
        private double _euroToRubbleRate = 59;
        private double _rubleToDollarRate = 1 / 56d;

        private bool _exitSignal = false;

        private readonly ExchangeRecordsContainer _exchangeRecordsContainer = new ExchangeRecordsContainer();

        private readonly Dictionary<Currency, double> _wallet = new Dictionary<Currency, double>();

        #region Enums

        private enum Currency
        {
            InvalidValue = -1,
            Euros = 0,
            Dollars = 1,
            Rubles = 2
        }

        private enum ConvertionResult
        {
            RecordNotFoundExeption = -2,
            InvalidOperation = -1,
            SameCurrency = 0,
            Success = 1
        }

        #endregion Enums

        public void Run()
        {
            _exitSignal = false;

            InitialiseMoneyBalance();
            InitializeExchangeRateRecords();

            const string ExitCommandInfo = "Для выхода наберите '" + ExitCommand + "'";

            string questionCurrencyToBuy = string.Format(
                "Введите валюту, которую желаете купить ( {0}. ).\n{1}.",
                GetCurrencyTypesInfo(), ExitCommandInfo);

            string questionCurrencyToSell = string.Format(
                "Введите валюту, которую желаете продать ( {0}. ).\n{1}.",
                GetCurrencyTypesInfo(), ExitCommandInfo);

            while (_exitSignal == false)
            {
                var currencyToBuy = GetСurrency(questionCurrencyToBuy);

                if (_exitSignal == true)
                {
                    break;
                }

                var currencyToSell = GetСurrency(questionCurrencyToSell);

                if (_exitSignal == true)
                {
                    break;
                }

                var amountOfCurrency = GetAmountOfMoney("Введите количество валюты, которые вы хотите купить.");

                if (_exitSignal == true)
                {
                    break;
                }

                double rate = 0;

                var rateRecordFounded = _exchangeRecordsContainer.TryGetExchangeRate(currencyToBuy, currencyToSell, out rate);

                if (rateRecordFounded)
                {
                    var totalSummNeeded = amountOfCurrency * rate;

                    if (_wallet[currencyToSell] >= totalSummNeeded)
                    {
                        _wallet[currencyToSell] -= totalSummNeeded;
                        _wallet[currencyToBuy] += amountOfCurrency;

                        ConsoleOutputMethods.Info("Сделка прошла успешно!");
                        PrintMoneyBalance();
                    }
                    else
                    {
                        ConsoleOutputMethods.Warning("Недостаточно денег на счету!");
                        ConsoleOutputMethods.Warning("Требуется валюты: "+totalSummNeeded);
                        PrintMoneyBalance();
                    }
                }
                else
                {
                    ConsoleOutputMethods.Warning("Обменный курс не найден. Сделка невозможна.");
                }
            }

            Console.WriteLine("Выход из программы.\nНажмите любую кнопку.");
            Console.ReadKey();
        }

        private void InitializeExchangeRateRecords()
        {
            _exchangeRecordsContainer.Clear();

            _exchangeRecordsContainer.AddRecord(new ExchangeRateRecord(Currency.Dollars, Currency.Euros, _euroToDollarRate));
            _exchangeRecordsContainer.AddRecord(new ExchangeRateRecord(Currency.Euros, Currency.Rubles, _euroToRubbleRate));
            _exchangeRecordsContainer.AddRecord(new ExchangeRateRecord(Currency.Rubles, Currency.Dollars, _rubleToDollarRate));

            ConsoleOutputMethods.Info("Курсы обмена валют установленны.");
            _exchangeRecordsContainer.PrintInfo();
            Console.WriteLine();
        }

        private void InitialiseMoneyBalance()
        {
            _wallet.Clear();

            _wallet.Add(Currency.Dollars, 100);
            _wallet.Add(Currency.Euros, 150);
            _wallet.Add(Currency.Rubles, 200);
            
            PrintMoneyBalance();
        }

        private void PrintMoneyBalance()
        {
            ConsoleOutputMethods.Info("Баланс денег:");
            foreach (var key in _wallet.Keys)
            {
                Console.WriteLine("{0}: {1:0.00}", key, _wallet[key]);
            }
            
            Console.WriteLine();
        }

        private string GetCurrencyTypesInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();

            string separator = ", ";

            var allowedCurrencyTypes = Enum.GetValues(typeof(Currency)).Cast<Currency>()
                        .Where(value => value != Currency.InvalidValue);


            foreach(var currencyType in allowedCurrencyTypes)
            {
                stringBuilder.AppendFormat("{0} - {1}"+separator, currencyType, (int)currencyType);
            }

            if (stringBuilder.Length > separator.Length)
            {
                stringBuilder.Remove(stringBuilder.Length-separator.Length,separator.Length);
            }

            return stringBuilder.ToString();
        }

        private Currency GetСurrency(string message)
        {
            var result = Currency.InvalidValue;
            var enumParsed = false;

            while (_exitSignal == false && enumParsed == false)
            {
                ConsoleOutputMethods.WriteLine(message, ConsoleColor.Green);
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    ConsoleOutputMethods.Warning("Введена пустая строка. Повторите ввод данных.");
                    continue;
                }

                if (input.Equals(ExitCommand))
                {
                    _exitSignal = true;
                    ConsoleOutputMethods.Info("Введена команда выхода.");
                    break;
                }

                var intParsed = Enum.TryParse(input, out result);

                if (intParsed == false)
                {
                    ConsoleOutputMethods.Warning("Введенная строка имеет неверный формат. Повторите ввод данных.");
                }
                else
                {
                    var allowedValues = Enum.GetValues(typeof(Currency)).Cast<Currency>()
                        .Where(value => value != Currency.InvalidValue);
                    
                    if (allowedValues.Contains(result))
                    {
                        Console.WriteLine("[{0}]", result);
                        enumParsed = true;
                    }
                    else
                    {
                        result = Currency.InvalidValue;
                        ConsoleOutputMethods.Warning("Указано неверное значение. Повторите ввод данных.");
                    }
                }
            }

            return result;
        }

        private double GetAmountOfMoney(string message)
        {
            double result = 0;
            var parsed = false;

            while (_exitSignal == false && parsed == false)
            {
                ConsoleOutputMethods.WriteLine(message, ConsoleColor.Green);
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    ConsoleOutputMethods.Warning("Введена пустая строка. Повторите ввод данных.");
                    continue;
                }

                if (input.Equals(ExitCommand))
                {
                    _exitSignal = true;
                    ConsoleOutputMethods.Info("Введена команда выхода.");
                    break;
                }

                parsed = double.TryParse(input, out result);

                if (parsed == false)
                {
                    ConsoleOutputMethods.Warning("Введенная строка имеет неверный формат. Повторите ввод данных.");
                }
            }

            return result;
        }

        #region Private Classes

        private class ExchangeRateRecord
        {
            public ExchangeRateRecord(Currency fistCurrency, Currency secondCurrency, double firstToSecondExchangeRate)
            {
                if (fistCurrency == Currency.InvalidValue || secondCurrency == Currency.InvalidValue)
                {
                    throw new ArgumentException("Ошибка при определении типа валюты.");
                }

                if (fistCurrency == secondCurrency)
                {
                    throw new InvalidOperationException("Значения типов валют должны быть различны.");
                }

                if (firstToSecondExchangeRate == 0)
                {
                    throw new ArgumentException("Стоимость конвертирования валют не может быть равна 0.");
                }

                FistCurrency = fistCurrency;
                SecondCurrency = secondCurrency;

                SecondToFirst = firstToSecondExchangeRate;
                FirstToSecond = 1 / firstToSecondExchangeRate;
            }

            public Currency FistCurrency { get; private set; }

            public Currency SecondCurrency { get; private set; }

            public double SecondToFirst { get; private set; }

            public double FirstToSecond { get; private set; }

            public void PrintInfo()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Первая валюта: " + FistCurrency.ToString());
                stringBuilder.AppendLine("Вторая валюта: " + SecondCurrency.ToString());
                stringBuilder.AppendLine("Стоимость второй валюты за первую: " + FirstToSecond);
                stringBuilder.AppendLine("Стоимость первой валюты за вторую: " + SecondToFirst);

                Console.WriteLine(stringBuilder.ToString());
            }
        }

        private class ExchangeRecordsContainer
        {
            public const int ExchangeRateDefaultValue = 1;

            private readonly List<ExchangeRateRecord> _records = new List<ExchangeRateRecord>();

            public bool TryGetExchangeRate(Currency currencyToBuy, Currency currencyToSell, out double rate)
            {
                foreach (var record in _records)
                {
                    if (record.FistCurrency == currencyToBuy && record.SecondCurrency == currencyToSell)
                    {
                        rate = record.SecondToFirst;
                        return true;
                    }
                    else if (record.FistCurrency == currencyToSell && record.SecondCurrency == currencyToBuy)
                    {
                        rate = record.FirstToSecond;
                        return true;
                    }
                }

                rate = ExchangeRateDefaultValue;
                return false;
            }

            public void AddRecord(ExchangeRateRecord record)
            {
                if (ContainsRecord(record) == false)
                {
                    _records.Add(record);
                }
                else
                {
                    Console.WriteLine("При добавлении в списки обменна валют следующая запись была проигнорирована.");
                    record.PrintInfo();
                }
            }
            public void Clear()
            {
                _records.Clear();
            }

            public void PrintInfo()
            {
                Console.WriteLine("Вывод записей о курсе валют.");

                for (int i = 0; i < _records.Count; i++)
                {
                    Console.WriteLine("Запись №'{0}' из '{1}'", i + 1, _records.Count);
                    _records[i].PrintInfo();
                }
            }

            private bool ContainsRecord(ExchangeRateRecord record)
            {
                return ContainsRecord(record.FistCurrency, record.SecondCurrency);
            }

            private bool ContainsRecord(Currency fistCurrency, Currency secondCurrency)
            {
                foreach (var record in _records)
                {
                    if ((record.FistCurrency == fistCurrency && record.SecondCurrency == secondCurrency) ||
                        (record.FistCurrency == secondCurrency && record.SecondCurrency == fistCurrency))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        #endregion Private Classes
    }
}

