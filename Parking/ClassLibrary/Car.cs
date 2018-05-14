using System;

namespace ClassLibrary
{
    public class Car : ICar
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public CarType TypeCar { get; set; }
    }

    public enum CarType
    {
        Undefined,
        Passenger,
        Truck,
        Bus,
        Motorcycle
    }
}
