using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationParking.Models
{
    public class MenuInfo
    {
        public string AddCar { get; private set; } = "api/Parking/AddCar";
        public string RemoveCar { get; private set; } = "api/Parking/RemoveCar/[Carid]";
        public string RefillBalance { get; private set; } = "api/Parking/RefillBalance/[Carid]";
        public string CarList { get; private set; } = "api/Parking/CarList";
        public string CarInfo { get; private set; } = "api/Parking/CarInfo/[Carid]";
        public string FreeParkingSpaces { get; private set; } = "api/Parking/FreeParkingSpaces";
        public string BusyParkingSpaces { get; private set; } = "api/Parking/BusyParkingSpaces";
        public string ParkingSpaces { get; private set; } = "api/Parking/ParkingSpaces";
        public string Balance { get; private set; } = "api/Transactions/Balance";
        public string TransactionHistory { get; private set; } = "api/Transactions/TransactionHistory";
        public string TransactionSum { get; private set; } = "api/Transactions/TransactionSum";
        public string TransactionHistoryCar { get; private set; } = "api/Transactions/TransactionHistoryCar/[Carid]";
        public string Menu { get; private set; } = "api/Menu";
    }
}
