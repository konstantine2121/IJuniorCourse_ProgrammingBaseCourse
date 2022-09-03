using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Существует продавец, он имеет у себя список товаров, и при нужде, может вам его показать, также продавец может продать вам товар. После продажи товар переходит к вам, и вы можете также посмотреть свои вещи.<br/>
    ///<br/>
    ///Возможные классы – игрок, продавец, товар.<br/>
    ///<br/>
    ///Вы можете сделать так, как вы видите это.
    /// </summary>
    class ShopTask : IRunnable
    {
        private InfoPrinter _printer;
        private Shop _shop;
        private Player _player;

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();

            _printer.PrintPlayerInfo(_player);
            _printer.PrintShopInfo(_shop);

            ConsoleOutputMethods.WriteLine("Пытаемся купить самую дорогую шмотку.", ConsoleColor.Cyan);
            Item mostExpensiveItem = _shop.ShowAllItems().Max();
            PrintPurchaseProcess(mostExpensiveItem);

            ConsoleOutputMethods.WriteLine("Пытаемся купить несуществующую в магазине шмотку.", ConsoleColor.Cyan);
            PrintPurchaseProcess(mostExpensiveItem);

            ConsoleOutputMethods.WriteLine("Опять пытаемся купить самую дорогую шмотку.", ConsoleColor.Cyan);
            mostExpensiveItem = _shop.ShowAllItems().Max();
            PrintPurchaseProcess(mostExpensiveItem);

            ConsoleOutputMethods.WriteLine("Пытаемся скупить все самые дешевые шмотки.", ConsoleColor.Cyan);
            var minPrice = _shop.ShowAllItems().Min(item => item.Price);
            var cheapItems = _shop.ShowAllItems().Where(item => item.Price == minPrice);
            PrintPurchaseProcess(cheapItems);
        }
        #endregion IRunnable Implementation

        private void Initialize()
        {
            _printer = new InfoPrinter();
            _shop = new ShopCreator().Create();
            _player = new Player(1400);
        }

        private void PrintPurchaseProcess(IEnumerable<Item> items)
        {
            PrintPurchaseProcess(items.ToArray());
        }

        private void PrintPurchaseProcess(params Item[] items)
        {
            var purchaseSuccess = _player.MakePurchase(items, _shop);

            if (purchaseSuccess)
            {
                ConsoleOutputMethods.Info("Покупка успешна.");
            }
            else
            {
                ConsoleOutputMethods.Warning("Покупка не удалась.");
            }

            _printer.PrintPlayerInfo(_player);
            _printer.PrintShopInfo(_shop);
        }


        #region Private Classes

        private class InfoPrinter
        {
            private const string Format = " {0, 6}  {1, -30}  {2}";

            public void PrintShopInfo(Shop shop)
            {
                ConsoleOutputMethods.Info("Информация о магазине.");
                Console.WriteLine("Баланс: " + shop.Balance);
                Console.WriteLine("Вещи в магазине:");
                PrintItemsInfo(shop.ShowAllItems());
                Console.WriteLine();
            }

            public void PrintPlayerInfo(Player player)
            {
                ConsoleOutputMethods.Info("Информация об игроке.");
                Console.WriteLine("Баланс: "+ player.Balance);
                Console.WriteLine("Вещи в сумке:");
                PrintItemsInfo(player.ShowAllItems());
                Console.WriteLine();
            }

            public void PrintItemsInfo(IEnumerable<Item> items)
            {
                Console.WriteLine(Format, "Цена", "Название", "Описание");
                foreach (var item in items)
                {
                    PrintItemInfo(item);
                }
            }

            public void PrintItemInfo(Item item)
            {                
                Console.WriteLine(Format, item.Price, item.Name, item.Description);
            }
        }
        
        private class Item : IComparable<Item>
        {
            /// <summary>
            /// Создаем новый товар.
            /// </summary>
            /// <param name="price">Цена.</param>
            /// <param name="name">Название.</param>
            /// <param name="description">Описание.</param>
            public Item(int price, string name, string description)
            {
                if (price < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(price));
                }

                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException(nameof(name));
                }

                Price = price;
                Name = name;
                Description = description;
            }

            public int Price { get; private set; }

            public string Name { get; private set; }

            public string Description { get; private set; }

            #region IComparable Implementation
            //https://docs.microsoft.com/ru-ru/dotnet/api/system.icomparable-1?view=net-6.0
            public int CompareTo(Item other)
            {   
                if (other == null) return 1;

                return Price.CompareTo(other.Price);
            }

            #endregion IComparable Implementation
        }

        private class Player
        {
            private readonly List<Item> _items = new List<Item>();

            public Player(int balance)
            {
                Balance = balance;
            }

            public int Balance { get; private set; } = 100;

            public bool MakePurchase(Item itemToBuy, Shop shop)
            { 
                return MakePurchase(new Item[] { itemToBuy }, shop);
            }

            public bool MakePurchase(IEnumerable<Item> itemsToBuy, Shop shop)
            {
                int moneyToPay = 0;

                if (shop.PerformSale(itemsToBuy, Balance, out moneyToPay))
                {
                    _items.AddRange(itemsToBuy);
                    Balance -= moneyToPay;

                    return true;
                }

                return false;
            }

            public IReadOnlyList<Item> ShowAllItems()
            {
                return _items;
            }
        }

        private class ShopCreator
        {
            public Shop Create()
            {
                int balance = 200;
                List<Item> items = new List<Item>();

                items.Add(new Item(200, "Малое зелье лечения", "Восстанавливает 10hp"));
                items.Add(new Item(200, "Малое зелье лечения", "Восстанавливает 10hp"));
                items.Add(new Item(500, "Среднее зелье лечения", "Восстанавливает 20hp"));
                items.Add(new Item(1000, "Большое зелье лечения", "Восстанавливает 40hp"));

                return new Shop(items, balance);
            }
        }

        private class Shop
        {
            private readonly List<Item> _items = new List<Item>();

            public Shop()
            {
            }

            public Shop(IEnumerable<Item> initialItems, int startBalance)
            {
                _items.AddRange(initialItems);
                Balance = startBalance;
            }

            public int Balance { get; private set; } = 100;

            public IReadOnlyList<Item> ShowAllItems()
            {
                return _items;
            }

            public bool PerformSale(IEnumerable<Item> itemsToBuy, int customerCash, out int moneyToPay)
            {   
                moneyToPay = 0;

                bool enougMoney = CheckSolvency(itemsToBuy, customerCash);
                bool allItemsAvailable = CheckAllItemsAvailable(itemsToBuy);

                if (enougMoney && allItemsAvailable)
                {
                    _items.RemoveAll(item => itemsToBuy.Contains(item));

                    moneyToPay = itemsToBuy.Sum(item => item.Price);
                    Balance += moneyToPay;

                    return true;
                }

                return false;
            }

            public bool PerformSale(Item itemToBuy, int customerCash, out int moneyToPay)
            {
                return PerformSale(new Item[] { itemToBuy }, customerCash, out moneyToPay);
            }

            private bool CheckSolvency(IEnumerable<Item> itemsToBuy, int customerCash)
            {
                if ( itemsToBuy.Sum(item => item.Price) > customerCash)
                {
                    return false;
                }
                return true;
            }

            private bool CheckAllItemsAvailable(IEnumerable<Item> itemsToBuy)
            { 
                foreach (var itemToBuy in itemsToBuy)
                {
                    if (_items.Contains(itemToBuy) == false)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        #endregion Private Classes
    }
}
