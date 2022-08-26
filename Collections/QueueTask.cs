using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.Collections
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///У вас есть множество целых чисел. Каждое целое число - это сумма покупки.<br/>
    ///Вам нужно обслуживать клиентов до тех пор, пока очередь не станет пуста.<br/>
    ///После каждого обслуженного клиента деньги нужно добавлять на наш счёт и выводить его в консоль.<br/>
    ///После обслуживания каждого клиента программа ожидает нажатия любой клавиши, после чего затирает консоль и по новой выводит всю информацию, только уже со следующим клиентом<br/>
    ///<br/>
    /// </summary>
    class QueueTask : IRunnable
    {
        #region IRunnable Implementation

        public void Run()
        {
            int balance = 100;

            Queue<int> buyers = new Queue<int>();
            InitQueue(buyers);

            Console.Clear();

            Console.WriteLine("Начальный баланс = {0}", balance);
            Console.WriteLine("В очереди = {0}", buyers.Count);

            Console.ReadKey();

            while (buyers.Count > 0)
            {
                balance += buyers.Dequeue();

                Console.Clear();
                
                Console.WriteLine("Баланс = {0}", balance);
                Console.WriteLine("В очереди = {0}", buyers.Count);                

                Console.ReadKey();
            }

            Console.WriteLine("Завершение работы.");
            Console.ReadKey();

        }

        #endregion IRunnable Implementation

        private void InitQueue(Queue<int> buyers)
        {
            int queueLengthFrom = 3;
            int queueLengthTo = 8;
            int minSum = 10;
            int maxSum = 50;
            Random random = new Random();

            int queueLength = random.Next(queueLengthFrom, queueLengthTo);

            for (int i=0;i < queueLength; i++)
            {
                buyers.Enqueue(random.Next(minSum, maxSum));
            }
        }
    }
}
