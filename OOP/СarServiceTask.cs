using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private CarService _carService;

        private enum DialogResult
        {
            Yes=1,
            No
        }

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();

            var carServiceWorking = true;
            var clientsQueque = new Queue<Client>();

            clientsQueque.Enqueue(new Client(new Detail(1, "главная", 10)));


            while (carServiceWorking
                && _carService.IsEmpty == false 
                && _carService.IsBankrupt == false
                && clientsQueque.Count > 0)
            {

            }
        }

        #endregion IRunnable Implementation

        private void Initialize()
        {
            //_carService = new СarService()
        }

        #region Private Classes

        private class Client
        {
            public Detail BrokenDetail { get; private set; }

            public Client(Detail detail)
            {
                BrokenDetail = detail;
            }
        }

        private class CarService
        {            
            private readonly Warehouse _warehouse;
            private readonly PriceList _priceList;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="balance"></param>
            /// <param name="priceList"></param>
            /// <param name="warehouse"></param>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public CarService(int balance, PriceList priceList, Warehouse warehouse)
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

            public bool CanRepair(int detailTypeId)
            {
                return _warehouse.CheckCanTakeDetail(detailTypeId);
            }

            public bool TryRepair(int detailTypeId)
            {
                return false;
            }

            public void ServeClient(Client client)
            {
                var detailToChange = client.BrokenDetail;

                //Show client detail.
                //Проверить прайс, что в нем есть такая услуга.
                //Если нету, то либо назначить новую цену за такую услугу и внести её в прайс лист, либо отказаться от сделки.


                //Принять решение менять или пойти в отказ.
                //Если менять
                //Выбрать деталь для замены
                //Взять её со склада.
                //Присобачить ее на место.
                //Проверить, ту ли я деталь присобачил.
            }



        }

        private class Warehouse
        {
            private const int NotFound = -1;

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
                var index = GetIndexOfRecord(detailTypeId);

                return index == NotFound ? false : _detailRecords[index].TakeDetail();
            }

            public bool CheckCanTakeDetail(int detailTypeId)
            {                
                var index = GetIndexOfRecord(detailTypeId);

                return index == NotFound ? false 
                    : _detailRecords[index].OutOfStock == false;
            }

            private int GetIndexOfRecord(int detailTypeId)
            {
                return _detailRecords.FindIndex(element => element.DetailType == detailTypeId);
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
            private Dictionary<int, int> _prices;

            public PriceList(Dictionary<int, int> prices)
            {
                _prices = new Dictionary<int, int>(prices);
            }

            public IReadOnlyDictionary<int, int> Records => _prices;

            public bool CheckIfExists(int detailTypeId)
            {
                return _prices.ContainsKey(detailTypeId);
            }

            public void AddNewService(int detailTypeId, int price)
            {
                _prices.Add(detailTypeId, price);
            }
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

        #region Creators

        private interface ICreator<T>
        {
            T Create();
        }

        private class RandomContainer
        {
            protected readonly Random Rand = new Random();
        }

        private class ClientsQuequeCreator: RandomContainer, ICreator<Queue<Client>>
        {
            //Не совпадает с CarServiceCreator.MaxDetailsTypes - так и задумано.
            private const int DetailsTypes = 10;

            private readonly List<Detail> _uniqueDetails;

            public Queue<Client> Create()
            {
                return null;
            }
        }

        private class CarServiceCreator : RandomContainer, ICreator<CarService>
        {
            //Число типов деталей
            private const int MinDetailsTypes = 3;
            private const int MaxDetailsTypes = 8;
            //Цена за деталь
            private const int MinDetailPrice= 100;
            private const int MaxDetailPrice= 2000;
            //Число деталей
            private const int MinNumberOfDetails = 5;
            private const int MaxNumberOfDetails = 25;
            //Цена за сервис
            private const int MinServicePrice = 50;
            private const int MaxServicePrice = 300;
            //Баланс автосервиса
            private const int MinBalance = 200;
            private const int MaxBalance = 1000;

            private readonly List<Detail> _uniqueDetails;

            public CarServiceCreator()
            {
                _uniqueDetails = new List<Detail>();
                FillUniqueDetails();
            }

            public CarService Create()
            {
                var detailList = new List<DetailRecord>();
                var priceDictionary = new Dictionary<int, int>();

                _uniqueDetails.ForEach(
                    detail =>
                    {
                        var numberOfDetails = Rand.Next(MinNumberOfDetails, MaxNumberOfDetails);
                        var servicePrice = Rand.Next(MinServicePrice, MaxServicePrice);

                        detailList.Add(new DetailRecord(detail, numberOfDetails));
                        priceDictionary.Add(detail.TypeId, servicePrice);
                    });

                var warehouse = new Warehouse(detailList);
                var priceList = new PriceList(priceDictionary);
                var balance = Rand.Next(MinBalance, MaxBalance);

                return new CarService(balance, priceList, warehouse);
            }

            private void FillUniqueDetails()
            {
                var defaultDetailName = "Деталь";
                var numberOfTypes = Rand.Next(MinDetailsTypes, MaxDetailsTypes);

                _uniqueDetails.Clear();
                
                for(int i=0; i< numberOfTypes; i++)
                {
                    var typeId = i + 1;
                    var price = Rand.Next(MinDetailPrice, MaxDetailPrice);

                    _uniqueDetails.Add(new Detail(typeId, defaultDetailName+" "+ typeId, price));
                }
            }
        }

        private class UniqueDetailsListCreator : RandomContainer
        {
            
        }


        #endregion Creators

        #endregion Private Classes
    }
}
