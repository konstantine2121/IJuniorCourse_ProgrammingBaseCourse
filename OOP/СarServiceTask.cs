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
        private CarService _carService;
        private Queue<Client> _clients;
        #region IRunnable Implementation

        public void Run()
        {
            Initialize();

            var carServiceWorking = true;

            while (carServiceWorking
                && _carService.IsEmpty == false 
                && _carService.IsBankrupt == false
                && _clients.Count > 0)
            {
                var client = _clients.Dequeue();
                _carService.ServeClient(client);
            }
        }

        #endregion IRunnable Implementation

        private void Initialize()
        {
            _carService = new CarServiceCreator().Create();
            _clients = new ClientsQuequeCreator().Create();


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
            private readonly DetailsRecordsContainer _warehouse;

            private ConsoleRecord _welcomeBar;
            private ConsoleRecord _balanceInfoBar;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="balance"></param>
            /// <param name="priceList"></param>
            /// <param name="warehouse"></param>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public CarService(int balance, DetailsRecordsContainer warehouse)
            {
                if (balance <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(balance));
                }

                Balance = balance;
                _warehouse = warehouse;

                PenaltyNotFound = 10;
                PenaltyWrongDetail = 50;

                InitializeViews();
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

                //Показать деталь клиента.
                //Проверить прайс, что в нем есть такая услуга.
                //Проверить записи со склада, что там есть информация о цене.
                //Вывести информацию о стоимости предстоящей работы (деталь+услуга).

                //Принять решение менять или пойти в отказ.
                //Если менять
                //Выбрать деталь для замены
                //Взять её со склада.
                //Присобачить ее на место.
                //Проверить, ту ли я деталь присобачил.
                UpdateViews();
            }

            #region UI

            private void InitializeViews()
            {
                _welcomeBar = new ConsoleRecord(20, 0);
                _welcomeBar.ForegroundColor = ConsoleColor.Green;
                _welcomeBar.Text = "Добро пожаловать в автомастерскую.";

                _balanceInfoBar = new ConsoleRecord(0, 1);
                _balanceInfoBar.ForegroundColor = ConsoleColor.Cyan;
                _balanceInfoBar.Text = "Баланс: "+ Balance;
            }
            
            private void UpdateViews()
            {
                _welcomeBar.Update();
                _balanceInfoBar.Update();
            }

            #endregion UI
        }

        private class DetailsRecordsContainer
        {
            private const int NotFound = -1;

            private List<DetailRecord> _detailRecords;

            public DetailsRecordsContainer(List<DetailRecord> detailRecords)
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
            public DetailRecord(Detail detail, int number, int detailPrice, int repairPrice)
            {
                if (detail == null)
                {
                    throw new ArgumentNullException(nameof(detail));
                }
                if (number < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(number));
                }
                if (detailPrice < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(detailPrice));
                }
                if (repairPrice < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(repairPrice));
                }

                Detail = detail;
                Counter = number;
                DetailPrice = detailPrice;
                RepairPrice = repairPrice;
            }

            public Detail Detail{get; private set;}

            public int DetailType => Detail.TypeId;
            public int Counter { get; private set; }

            public int DetailPrice { get; private set; }

            public int RepairPrice { get; private set; }

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

        private class Detail
        {
            /// <summary>
            /// Создаем новый эекзмпляр детали.
            /// </summary>
            /// <param name="typeId">Идентификатор типа.</param>
            /// <param name="name">Название детали.</param>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="ArgumentException"></exception>
            public Detail(int typeId, string name)
            {
                if (typeId < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(typeId));
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
            }

            public int TypeId { get; private set; }

            public string Name { get; private set; }
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

            private const int MinNumberOfClients = 10;
            private const int MaxNumberOfClients = 100;

            private readonly List<Detail> _uniqueDetails;
            private readonly UniqueDetailsListCreator _detailsListCreator;

            public ClientsQuequeCreator()
            {
                _detailsListCreator = new UniqueDetailsListCreator(DetailsTypes);
                _uniqueDetails = _detailsListCreator.Create();
            }

            public Queue<Client> Create()
            {
                var numberOfClients = Rand.Next(MinNumberOfClients, MaxNumberOfClients);
                var clients = new Queue<Client>();

                for (int i =0; i< numberOfClients; i++)
                {
                    clients.Enqueue(CreateClient());
                }

                return clients;
            }

            private Client CreateClient()
            {
                var index = Rand.Next(_uniqueDetails.Count);

                return new Client(_uniqueDetails[index]);
            }
        }

        private class CarServiceCreator : RandomContainer, ICreator<CarService>
        {
            //Цена за деталь
            private const int MinDetailPrice = 100;
            private const int MaxDetailPrice = 2000;
            //Число типов деталей
            private const int MinDetailsTypes = 3;
            private const int MaxDetailsTypes = 8;
            //Число деталей
            private const int MinNumberOfDetails = 5;
            private const int MaxNumberOfDetails = 25;
            //Цена за сервис
            private const int MinServicePrice = 50;
            private const int MaxServicePrice = 300;
            //Баланс автосервиса
            private const int MinBalance = 200;
            private const int MaxBalance = 1000;

            private readonly UniqueDetailsListCreator _detailsListCreator;

            public CarServiceCreator()
            {
                _detailsListCreator = new UniqueDetailsListCreator(MinDetailsTypes);
            }

            public CarService Create()
            {
                _detailsListCreator.NumberOfTypes = Rand.Next(MinDetailsTypes, MaxDetailsTypes);

                var uniqueDetails = _detailsListCreator.Create();
                var detailList = new List<DetailRecord>();

                uniqueDetails.ForEach(
                    detail =>
                    {
                        var numberOfDetails = Rand.Next(MinNumberOfDetails, MaxNumberOfDetails);
                        var servicePrice = Rand.Next(MinServicePrice, MaxServicePrice);
                        var price = Rand.Next(MinDetailPrice, MaxDetailPrice);

                        detailList.Add(new DetailRecord(detail, numberOfDetails, price, servicePrice));
                    });

                var warehouse = new DetailsRecordsContainer(detailList);                
                var balance = Rand.Next(MinBalance, MaxBalance);

                return new CarService(balance, warehouse);
            }
        }

        private class UniqueDetailsListCreator : RandomContainer, ICreator<List<Detail>>
        {

            private const string DefaultDetailName = "Деталь";

            private List<Detail> _uniqueDetails;
            private int _numberOfTypes;

            public UniqueDetailsListCreator(int numberOfTypes)
            {
                NumberOfTypes = numberOfTypes;
            }

            public int NumberOfTypes
            {
                get
                {
                    return _numberOfTypes;
                }
                set
                {
                    if (value >= 1)
                    {
                        _numberOfTypes = value;
                    }
                    else
                    {
                        _numberOfTypes = 1;
                    }
                }
            }

            public List<Detail> Create()
            {
                _uniqueDetails = new List<Detail>();


                for (int i = 0; i < NumberOfTypes; i++)
                {
                    var typeId = i + 1;
                    _uniqueDetails.Add(new Detail(typeId, DefaultDetailName + " " + typeId));
                }

                return _uniqueDetails;
            }
        }


        #endregion Creators

        #endregion Private Classes
    }
}
