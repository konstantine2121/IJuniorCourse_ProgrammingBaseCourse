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
    ///У вас есть программа, которая помогает пользователю составить план поезда.<br/>
    ///Есть 4 основных шага в создании плана:<br/>
    ///-Создать направление - создает направление для поезда(к примеру Бийск - Барнаул)<br/>
    ///-Продать билеты - вы получаете рандомное кол-во пассажиров, которые купили билеты на это направление<br/>
    ///-Сформировать поезд - вы создаете поезд и добавляете ему столько вагонов(вагоны могут быть разные по вместительности), сколько хватит для перевозки всех пассажиров.<br/>
    ///-Отправить поезд - вы отправляете поезд, после чего можете снова создать направление.<br/>
    ///В верхней части программы должна выводиться полная информация о текущем рейсе или его отсутствии.
    /// </summary>
    class TrainConfiguratorTask :IRunnable
    {
        private const int MaxPassengersOnTrain = 400;

        private ConsoleRecord _currentRouteInfo = new ConsoleRecord(0,0);
        private ConsoleRecord _numberOfSeatsInfo = new ConsoleRecord(80,0);
        private ConsoleRecord _numberOfPassengersInfo = new ConsoleRecord(80,1);
        private TrainWagon _standartWagon;
        private int _tempCursorX;
        private int _tempCursorY;
        private Random _random = new Random();

        #region IRunnable Implementation

        public void Run()
        {
            Initialize();

            int outputColumnStart = 0;
            int outputRowStart = 3;

            //В условиях задачи ничего не сказано про то - когда стоит прервать цикл...
            bool userIsTired = false;

            while (userIsTired == false)
            {
                Console.Clear();
                UpdateInfobars();
                Console.SetCursorPosition(outputColumnStart, outputRowStart);
                Console.WriteLine();

                var train = MakePlan();
                _currentRouteInfo.Text = GetTrainDescription(train);
            }
            Console.ReadKey();
        }

        #endregion IRunnable Implementation

        private void Initialize()
        {
            int numberOfSeats = ConsoleInputMethods.ReadPositiveInteger("Введите количество мест в одном вагоне: ");

            _standartWagon = new TrainWagon(numberOfSeats);

            _numberOfSeatsInfo.Text = "Кол-во мест в вагоне: "+numberOfSeats;
            UpdateInfobars();
        }

        private void UpdateInfobars()
        {
            SaveCursorPosition();
            _currentRouteInfo.Update();
            _numberOfSeatsInfo.Update();
            _numberOfPassengersInfo.Update();
            RestoreCursorPosition();
        }

        /// <summary>
        ///Создать направление<br/>
        ///Продать билеты<br/>
        ///Сформировать поезд<br/>
        ///Отправить поезд
        /// </summary>
        /// <returns></returns>
        private Train MakePlan()
        {
            ConsoleOutputMethods.Info("Составление плана:");

            var route = CreateTrainRoute();
            var passengers = SellTickets();            
            var train = new Train(CalculateNumberOfWagons(passengers), route);

            ConsoleOutputMethods.Info($"Купили билеты {passengers} пассажиров.");
            _numberOfPassengersInfo.Text = "Кол-во пассажиров: " + passengers;
            _currentRouteInfo.Text = GetTrainDescription(train);
            UpdateInfobars();
            
            ConsoleOutputMethods.Warning("Нажмите Enter, чтобы отправить поезд по маршруту.");
            Console.ReadLine();            
            train.SendOnTheWay();

            return train;
        }

        private TrainRoute CreateTrainRoute()
        {
            ConsoleOutputMethods.Info("Создание маршрута.");
            var departure = ConsoleInputMethods.ReadString("Введите точку отправления: ");
            var destination = ConsoleInputMethods.ReadString("Введите точку назначения: ");

            return new TrainRoute(departure, destination);
        }

        private int SellTickets()
        {
            return _random.Next(MaxPassengersOnTrain);
        }

        private int CalculateNumberOfWagons(int passengers)
        {
            int result = (int) (passengers /(double)_standartWagon.NumberOfSeats);
            
            if (passengers % _standartWagon.NumberOfSeats != 0)
            {
                result++;
            }

            return result;
        }

        private string GetTrainDescription(Train train)
        {
            const string format = "{0,15}\t{1,15}\t{2,15}\t{3, 15}";
            string headers = string.Format(format,"Откуда", "Куда", "Кол-во вагонов", "В пути");
            string info = string.Format(format,train.Route.Departure, train.Route.Destination, train.NumberOfWagons, train.OnTheWay);
            return string.Concat(headers, "\n", info, "\n");
        }

        private void SaveCursorPosition()
        {
            _tempCursorX = Console.CursorLeft;
            _tempCursorY = Console.CursorTop;
        }

        private void RestoreCursorPosition()
        {
            Console.CursorLeft = _tempCursorX;
            Console.CursorTop = _tempCursorY;
        }

        #region Private Classes

        private class TrainRoute
        {
            public TrainRoute(string departure, string destination)
            {
                Departure = departure;
                Destination = destination;
            }

            /// <summary>
            /// Пункт назначения.
            /// </summary>
            public string Destination { get; private set; }

            /// <summary>
            /// Точка отправления
            /// </summary>
            public string Departure { get; private set; }
        }

        private class TrainWagon
        {
            public TrainWagon(int numberOfSeats)
            {
                NumberOfSeats = numberOfSeats;
            }

            public int NumberOfSeats { get; private set; }
        }

        private class Train
        {
            public Train(int numberOfWagons, TrainRoute trainRoute)
            {
                NumberOfWagons = numberOfWagons;
                Route = trainRoute;
            }

            public bool OnTheWay { get; private set; } = false;

            public int NumberOfWagons { get; private set; }

            public TrainRoute Route { get; private set; }

            public void SendOnTheWay ()
            {
                OnTheWay = true;
            }
        }

        private class ConsoleRecord
        {
            public ConsoleRecord(int cursorLeft, int cursorTop)
            {
                CursorLeft = cursorLeft;
                CursorTop = cursorTop;
            }

            public string Text { get; set; } = string.Empty;

            public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;

            public int CursorLeft { get; private set; }

            public int CursorTop { get; private set; }

            public virtual void Update()
            {
                ConsoleColor tempColor = Console.ForegroundColor;
                Console.ForegroundColor = ForegroundColor;

                int positionY = CursorTop;

                var lines = Text.Split(new[] { '\r', '\n' }, StringSplitOptions.None);

                foreach (var line in lines)
                {
                    Console.SetCursorPosition(CursorLeft, positionY);
                    Console.Write(line);
                    positionY++;
                }

                Console.ForegroundColor = tempColor;
            }
        }

        #endregion Private Classes

    }
}
