using System;
using System.Collections.Generic;
using System.Linq;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using IJuniorCourse_ProgrammingBaseCourse.CommonViews;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Написать программу администрирования супермаркетом.<br/>
    ///В супермаркете есть очередь клиентов.<br/>
    ///У каждого клиента в корзине есть товары, также у клиентов есть деньги.<br/>
    ///Клиент, когда подходит на кассу, получает итоговую сумму покупки и старается её оплатить.<br/>
    ///Если оплатить клиент не может, то он рандомный товар из корзины выкидывает до тех пор, пока его денег не хватит для оплаты.<br/>
    ///Клиентов можно делать ограниченное число на старте программы.
    /// </summary>
    class SupermarketAdministrationTask : IRunnable
    {
        private Supermarket _supermarket;

        #region IRunnable Implementation

        public void Run()
        {
            _supermarket = new SupermarketCreator().Create();
            _supermarket.ServeQueque();
        }

        #endregion IRunnable Implementation

        #region Private Classes

        interface ISalePerformer
        {
            bool PerformSale(IReadOnlyShoppingCart basket, int customerCash, out int moneyToPay);
        }

        private class Supermarket : ISalePerformer
        {
            private readonly List<Item> _droppedItems = new List<Item>();
            private readonly Queue<Client> _clientsQueue;

            private ConsoleRecord _queueInfoBar;
            private ConsoleRecord _currentClientInfoBar;
            private ConsoleRecord _balanceInfoBar;
            private ConsoleRecord _droppedItemsInfoBar;

            public Supermarket(Queue<Client> clientsQueue)
            {
                _clientsQueue = clientsQueue;
                InitViews();
            }

            public int Balance { get; private set; }

            public int ClientsQueueLength { get { return _clientsQueue.Count; } }

            public IReadOnlyList<Item> DroppedItems { get { return _droppedItems; } }

            public IReadOnlyList<Client> GetClients()
            {
                return _clientsQueue.ToList();
            }

            public bool PerformSale(IReadOnlyShoppingCart basket, int customerCash, out int moneyToPay)
            {
                moneyToPay = 0;

                bool enougMoney = CheckSolvency(basket, customerCash);

                if (enougMoney)
                {                    
                    moneyToPay = basket.ReadOnlyItems.Sum(item => item.Price);
                    Balance += moneyToPay;

                    return true;
                }

                return false;
            }

            public void ServeQueque()
            {
                while (ClientsQueueLength > 0)
                {
                    ServeClient();
                    Console.WriteLine();

                    if (ClientsQueueLength > 0)
                    {
                        ConsoleOutputMethods.Warning("Нажмите любую кнопку, чтобы обслужить следующего клиента.");
                        Console.ReadKey();
                    }
                }
                ConsoleOutputMethods.Info("Клиентов в очереди не осталось.");
                Console.ReadKey();
            }

            private void ServeClient()
            {
                var client = _clientsQueue.Dequeue();

                var purchaseSuccess = false;
                var needToCallPolice = false;//это флаг по фану

                UpdateClientInfo(client);
                UpdateInfoBars();

                while (purchaseSuccess == false && needToCallPolice == false)
                {
                    purchaseSuccess = client.MakePurchase(this);

                    if (purchaseSuccess == false)
                    {
                        var removed =  client.RemoveOneItem(out Item droppedItem);

                        if (removed == false)
                        {
                            //Это возможно если у клиента отрицательный баланс(карма). Тогда нужно отобрать пустую корзинку и вызвать полицию.
                            needToCallPolice = true;
                        }
                        else
                        {
                            _droppedItems.Add(droppedItem);
                        }
                    }
                }
            }

            private bool CheckSolvency(IReadOnlyShoppingCart basket, int customerCash)
            {
                return basket.ReadOnlyItems.Sum(item => item.Price) <= customerCash;
            }

            private void InitViews()
            {
                _queueInfoBar = new ConsoleRecord(0, 1);
                _currentClientInfoBar = new ConsoleRecord(0,2);
                _balanceInfoBar = new ConsoleRecord(0, 3);
                _droppedItemsInfoBar = new ConsoleRecord(0, 4);
            }

            private void UpdateInfoBars()
            {
                Console.Clear();

                _queueInfoBar.Text = "Всего в очереди: " + ClientsQueueLength + " человек.";
                _balanceInfoBar.Text = "Баланс супермаркета: " + Balance;
                _droppedItemsInfoBar.Text = "Всего возвращено вещей: " + DroppedItems.Count;

                _queueInfoBar.Update();
                _currentClientInfoBar.Update();
                _balanceInfoBar.Update();
                _droppedItemsInfoBar.Update();
            }

            private void UpdateClientInfo(Client client)
            {
                _currentClientInfoBar.Text = "Денег у клиента: " + client.Balance + "\t\tПредметов в корзине: " + client.Cart.ReadOnlyItems.Count;
            }
        }

        private class Client
        {
            private readonly Random _random = new Random();
            private ShoppingCart _cart;

            public Client(ShoppingCart shoppingCart, int balance)
            {
                if (balance < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(balance));
                }

                _cart = shoppingCart;
                Balance = balance;
            }

            public int Balance { get; private set; }

            public IReadOnlyShoppingCart Cart => _cart;

            public bool MakePurchase(ISalePerformer shop)
            {
                int moneyToPay = 0;

                if (shop.PerformSale(Cart, Balance, out moneyToPay))
                {
                    Balance -= moneyToPay;

                    return true;
                }

                return false;
            }

            public bool RemoveOneItem(out Item droppedItem)
            {
                droppedItem = null;

                if (_cart.Items.Count == 0)
                {
                    return false;
                }

                int index = _random.Next(_cart.Items.Count);
                droppedItem = _cart.Items[index];
                _cart.Items.RemoveAt(index);

                return true;
            }
        }

        private interface IReadOnlyShoppingCart
        {
            IReadOnlyList<Item> ReadOnlyItems { get; }
        }

        private class ShoppingCart : IReadOnlyShoppingCart
        {
            //Покупатель сам ковыряет свою корзину.
            public List<Item> Items;

            public ShoppingCart(IEnumerable<Item> items)
            {
                Items = items.ToList();
            }

            //А так он её апоказывает посторонним
            public IReadOnlyList<Item> ReadOnlyItems => Items;
        }

        private class Item
        {
            public Item(int price)
            {
                Price = price;
            }

            public int Price { get; private set; }
        }

        #region Creators
        
        private interface ICreator <T>
        {
            T Create();
        }

        private class RandomContainer
        {
            protected readonly Random Rand = new Random();
        }

        private class SupermarketCreator : RandomContainer, ICreator<Supermarket>
        {
            public const int MinQuequeLength = 10;
            public const int MaxQuequeLength = 100;

            private ClientCreator _creator = new ClientCreator();
                        
            public Supermarket Create()
            {
                Queue<Client> clientsQueque = new Queue<Client>();

                int numberOfClients = Rand.Next(MinQuequeLength, MaxQuequeLength);

                for(int i=0; i< numberOfClients; i++)
                {
                    clientsQueque.Enqueue(_creator.Create());
                }

                return new Supermarket(clientsQueque);
            }
        }

        private class ClientCreator : RandomContainer, ICreator<Client>
        {
            public const int MinBalance = 1;
            public const int MaxBalance = 2500;

            private readonly ShoppingCartCreator _creator = new ShoppingCartCreator();

            public Client Create()
            {
                return new Client(_creator.Create(), Rand.Next(MinBalance,MaxBalance));
            }
        }

        private class ShoppingCartCreator : RandomContainer, ICreator<ShoppingCart>
        {
            public const int MinItems = 1;
            public const int MaxItems = 40;

            private readonly ItemCreator _creator = new ItemCreator();
            
            public ShoppingCart Create()
            {
                int itemsNumber = Rand.Next(MinItems, MaxItems);
                var items = new List<Item>();

                for (int i=0;i< itemsNumber; i++)
                {
                    items.Add(_creator.Create());
                }

                return new ShoppingCart(items);
            }
        }

        private class ItemCreator : RandomContainer, ICreator<Item>
        {
            public const int MinPrice = 1;
            public const int MaxPrice = 300;

            public Item Create()
            {
                var price = Rand.Next(MinPrice, MaxPrice);

                return new Item(price);
            }
        }

        #endregion Creators

        #endregion Private Classes
    }
}
