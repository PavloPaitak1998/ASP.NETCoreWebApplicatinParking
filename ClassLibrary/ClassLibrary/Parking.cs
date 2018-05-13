using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Parking
    {
        public static Dictionary<int, Timer> TimersDictionary = new Dictionary<int, Timer>();
        public static Timer timerTransactions = null;

        private readonly object locker = new object();
        private int start;
        private int count;

        private int numberOfCars;
        private string SumTransactionsFilePath;

        public int FreeParkingSpace { get; private set; }
        public int BusyParkingSpace { get; private set; }

        public List<ICar> Cars { get; private set; }
        public List<Transaction> Transactions { get; private set; }
        public double Balance { get; private set; }

        public Parking()
        {
            Cars = new List<ICar>();
            Transactions = new List<Transaction>();
            Balance = 0;
            FreeParkingSpace = Settings.ParkingSpace;
            BusyParkingSpace = 0;
            numberOfCars = 0;
            SumTransactionsFilePath = "Transaction.log";
            start = 0;
            count = 0;
        }

        public void AddCar(ICar car)
        {
            if (car == null)
            {
                throw new NullReferenceException("Sth went wrong please input Car information again!");
            }
            else if (Exist(car))
            {
                throw new CarAlreadyExistException($"Car with this Id: {car.Id} and Type: {car.TypeCar} already exist. Please try to input another car information!");
            }
            else if (numberOfCars == Settings.ParkingSpace)
            {
                throw new FullParkingSpaces("Parking Spaces is full. Try later!");
            }

            Cars.Add(car as Car);

            Timer timer = new Timer(Charge, car, Settings.TimeOut, Settings.TimeOut);
            TimersDictionary.Add(car.Id, timer);

            if (timerTransactions == null)
            {
                timerTransactions = new Timer(WriteTransactions, null, 0, 60000);
            }

            BusyParkingSpace++;
            FreeParkingSpace--;
            numberOfCars++;
        }

        public void RemoveCar(ICar car)
        {
            if (car == null)
            {
                throw new NullReferenceException("Sth went wrong please input Car information again!");
            }
            else if (car.Balance < 0)
            {
                throw new OutOfBalanceException("Car balance is out you have to refill Balance");
            }
            Cars.Remove(car as Car);

            BusyParkingSpace--;
            FreeParkingSpace++;
            numberOfCars--;
        }

        public void RefillBalance(ICar car, double balance)
        {
            if (car == null)
            {
                throw new NullReferenceException();
            }

            var index = Cars.IndexOf(car as Car);

            Cars[index].Balance += balance;
        }

        public void Charge(object obj)
        {
            if (obj as Car == null)
            {
                throw new Exception();
            }

            var car = obj as Car;

            if (!Exist(car))
            // Dispose Requested.  
            {
                TimersDictionary[car.Id].Dispose();
                TimersDictionary.Remove(car.Id);
            }

            double payment = 0.0;

            if (car.Balance < Settings.Price[car.TypeCar])
            {
                payment = Settings.Price[car.TypeCar] * Settings.Fine;
                car.Balance -= payment;
            }
            else
            {
                payment = Settings.Price[car.TypeCar];
                car.Balance -= payment;
            }

            lock (locker)
            {
                Balance += payment;

                Transactions.Add(new Transaction
                {
                    TransactionTime = DateTime.Now,
                    CarId = car.Id,
                    Payment = payment
                });
            }
        }

        public void WriteTransactions(object obj)
        {
            if (BusyParkingSpace == 0)
            // Dispose Requested.  
            {
                timerTransactions.Dispose();
            }

            count = Transactions.Count - start;

            var transactions = Transactions.GetRange(start, count);

            start = transactions.Count;

            using (StreamWriter sw = new StreamWriter(SumTransactionsFilePath, false, System.Text.Encoding.Default))
            {
                double sum = 0;
                sum = transactions.Sum(t => t.Payment);
                sw.WriteLine("TransactionTime: " + DateTime.Now + " Sum: " + sum);
            }
        }

        //public Task<string> ReadSumTransactions()
        //{
        //    string transactionSum;
        //    return Task.Run(()=>
        //    {
        //        using (StreamReader sr = new StreamReader(SumTransactionsFilePath))
        //        {
        //            transactionSum = sr.ReadLine();
        //        }
        //        return transactionSum;
        //    });
        //}

        public string ReadSumTransactions()
        {
            string transactionSum;
            using (StreamReader sr = new StreamReader(SumTransactionsFilePath))
            {
                transactionSum = sr.ReadLine();
            }
            return transactionSum;

        }

        public bool Exist(ICar car)
        {
            return Cars.Exists(c => c.Id == car.Id && c.TypeCar == car.TypeCar);
        }
    }
}
