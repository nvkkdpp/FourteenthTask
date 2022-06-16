using System;
using System.Collections.Generic;

namespace FourteenthTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Создание очереди с приоритетами:");
            PriorityQueue<Package> pq = new PriorityQueue<Package>();
            Console.WriteLine("Добавляю случайное кол-во (0-20) случайных пакетов в очередь");
            Package pack;
            PackageFactory fact = new PackageFactory();
            Random rand = new Random();
            int numToCreate = rand.Next(20);
            Console.WriteLine("\tСоздание {0} пакетов:", numToCreate);
            for (int i = 0; i < numToCreate; i++)
            {
                Console.Write("\t\tГенерация и добавление случайного пакета {0}", i);
                pack = fact.CreatePackage();
                Console.WriteLine("С приоритетом {0}", pack.Priority);
                pq.Enqueue(pack);
            }
            Console.WriteLine("Получилось: ");
            int total = pq.Count;
            Console.WriteLine("Получено пакетов:{0}", total);
            Console.WriteLine("Извлекаем случайное кол-во пакетов: 0-20:");
            int numToRemove = rand.Next(20);
            Console.WriteLine("\tИзвлeкaeм {0} пакетов", numToRemove);
            for (int i = 0; i < numToRemove; i++)
            {
                pack = pq.Dequeue();
                if (pack != null)
                {
                    Console.WriteLine("\t\tДоставка пакета с приоритетом {0}", pack.Priority);
                }
            }
            Console.WriteLine("Доставлено {0} пакетов", total - pq.Count);
            Console.WriteLine("Нажмите <enter> для завершения программы . . .");
            Console.Read();
        }
    }
    enum Priority
    {
        Low, Medium, High
    }
    interface IPriotitizable
    {
        Priority Priority { get; }
    }
    class PriorityQueue<T>
        where T : IPriotitizable
    {
        private Queue<T> _queueHigh = new Queue<T>();
        private Queue<T> _queueMedium = new Queue<T>();
        private Queue<T> _queueLow = new Queue<T>();

        public void Enqueue(T item)
        {
            if (Priority.High == item.Priority)
            {
                _queueHigh.Enqueue(item);
            }
            if (Priority.Medium == item.Priority)
            {
                _queueMedium.Enqueue(item);
            }
            if (Priority.Low == item.Priority)
            {
                _queueLow.Enqueue(item);
            }
            switch (item.Priority)
            {
                case Priority.High:
                    _queueHigh.Enqueue(item);
                    break;
                case Priority.Low:
                    _queueLow.Enqueue(item);
                    break;
                case Priority.Medium:
                    _queueMedium.Enqueue(item);
                    break;
                default:
                    throw new
                     ArgumentOutOfRangeException(item.Priority.ToString(), "Неверный приоритет в PriorityQueue.Enqueue");
            }
    }
        public T Dequeue()
        {
            Queue<T> queueTop = TopQueue();
            if (queueTop != null && queueTop.Count > 0)
            {
                return queueTop.Dequeue();
            }
            return default(T);
        }
        private Queue<T> TopQueue()
        {
            if (_queueHigh.Count > 0)
                return _queueHigh;
            if (_queueMedium.Count > 0)
                return _queueMedium;
            if (_queueLow.Count > 0)
                return _queueLow;
            return _queueLow;
        }
        public bool IsEmpty()
        {
            return (_queueHigh.Count == 0) &
                   (_queueMedium.Count == 0) &
                   (_queueLow.Count == 0);

        }
        public int Count
        {
            get { return _queueHigh.Count + _queueMedium.Count + _queueLow.Count; }

        }

    }
    class Package : IPriotitizable
    {
        private Priority priority;
        public Package(Priority priority)
        {
            this.priority = priority;
        }
        public Priority Priority
        {
            get { return priority; }
        }
    }
    class PackageFactory : PackageMactory
    {
       
        public Package CreatePackage()
        {
            int rand = _randGen.Next(3);
            return new Package((Priority)rand);
        }
    }
    class PackageMactory
    {
       public Random _randGen = new Random();
    }
}