using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.CommonViews;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///У вас есть автосервис, в который приезжают люди, чтобы починить свои автомобили.<br/>
    ///У вашего автосервиса есть баланс денег и склад деталей.<br/>
    ///Когда приезжает автомобиль, у него сразу ясна его поломка, <br/>
    ///и эта поломка отображается у вас в консоли вместе с ценой за починку<br/>
    ///(цена за починку складывается из цены детали + цена за работу).<br/>
    ///Поломка всегда чинится заменой детали, но количество деталей ограничено тем,<br/>
    /// что находится на вашем складе деталей.<br/>
    ///Если у вас нет нужной детали на складе, то вы можете отказать клиенту,<br/>
    /// и в этом случае вам придется выплатить штраф.<br/>
    ///Если вы замените не ту деталь, то вам придется возместить ущерб клиенту.<br/>
    ///За каждую удачную починку вы получаете выплату за ремонт, которая указана в чек-листе починки.<br/>
    ///Класс Деталь не может содержать значение “количество”. <br/>
    ///Деталь всего одна, за количество отвечает тот, кто хранит детали.
    /// </summary>
    class СarServiceTask : IRunnable
    {

        private ConsoleRecord _welcomeBar;
        private ConsoleRecord _balanceInfoBar;

        private СarService _carService;

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();

            var carServiceWorking = true;

            while (carServiceWorking
                && _carService.IsEmpty == false 
                && _carService.IsBankrupt == false)
            {

            }
        }

        #endregion IRunnable Implementation

        private void Initialize()
        {

        }

        #region Private Classes

        private class СarService
        {            
            private readonly Warehouse _warehouse;
            private readonly PriceList _priceList;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="balance"></param>
            /// <param name="priceList"></param>
            /// <param name="warehouse"></param>
            public СarService(int balance, PriceList priceList, Warehouse warehouse)
            {
                if (balance <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(balance));
                }

                Balance = balance;
                _priceList = priceList;
                _warehouse = warehouse;

                PenaltyNotFound = 10;
                PenaltyWrongDetail = 50;
            }

            public int Balance { get; private set; }

            public int PenaltyNotFound{ get; private set; }

            public int PenaltyWrongDetail{ get; private set; }

            public bool IsBankrupt => Balance <= 0;

            public bool IsEmpty => _warehouse.IsEmpty;

        }

        private class Warehouse
        {
            private List<DetailRecord> _detailRecords;

            public Warehouse(List<DetailRecord> detailRecords)
            {
                _detailRecords = new List<DetailRecord>();
                _detailRecords.AddRange(detailRecords);
            }

            public IEnumerable<DetailRecord> Records => _detailRecords;

            public bool IsEmpty => _detailRecords.All(record => record.OutOfStock);

            public bool TakeDetail(int detailTypeId)
            {
                const int notFound = -1;
                var index = _detailRecords.FindIndex(element => element.DetailType == detailTypeId);

                return index == notFound ? false : _detailRecords[index].TakeDetail();
            }
        }

        private struct DetailRecord
        {
            /// <summary>
            /// Создать запись о детали.
            /// </summary>
            /// <param name="detail">Деталь.</param>
            /// <param name="number">Количество.</param>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            /// 
            public DetailRecord(Detail detail, int number)
            {
                if (detail == null)
                {
                    throw new ArgumentNullException(nameof(detail));
                }
                if (number < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(number));
                }

                Detail = detail;
                Counter = number;
            }

            public Detail Detail{get; private set;}

            public int DetailType => Detail.TypeId;

            public int Counter { get; private set; }

            public bool OutOfStock => Counter <= 0;

            public bool TakeDetail()
            {
                if (OutOfStock)
                {
                    return false;
                }
                Counter--;

                return true;
            }
        }

        private class PriceList
        {
            private List<PriceRecord> _prices;

            public PriceList(IEnumerable<PriceRecord> prices)
            {
                _prices = new List<PriceRecord>();
                _prices.AddRange(prices);
            }

            public IReadOnlyList<PriceRecord> Records => _prices;
        }

        private struct PriceRecord
        {
            public PriceRecord(int detailTypeId, int price)
            {
                if (price <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(price));
                }

                DetailTypeId = detailTypeId;
                Price = price;
            }

            public int DetailTypeId { get; private set; }

            public int Price { get; private set; }
        }

        private class Detail
        {
            /// <summary>
            /// Создаем новый эекзмпляр детали.
            /// </summary>
            /// <param name="typeId">Идентификатор типа.</param>
            /// <param name="name">Название детали.</param>
            /// <param name="price">Цена.</param>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="ArgumentException"></exception>
            public Detail(int typeId, string name, int price)
            {
                if (typeId < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(typeId));
                }

                if (price < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(price));
                }

                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException("Имя не может быть пустым.");
                }

                TypeId = typeId;
                Name = name;
                Price = price;
            }

            public int TypeId { get; private set; }

            public string Name { get; private set; }

            public int Price { get; private set; }
        }

        #endregion Private Classes
    }
}
