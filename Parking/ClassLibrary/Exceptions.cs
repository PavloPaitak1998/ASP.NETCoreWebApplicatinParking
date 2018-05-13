using System;

namespace ClassLibrary
{
    public class OutOfBalanceException : Exception
    {
        public OutOfBalanceException(string message)
       : base(message)
        { }
    }

    public class CarNotExistException : Exception
    {
        public CarNotExistException(string message)
       : base(message)
        { }
    }

    public class CarAlreadyExistException : Exception
    {
        public CarAlreadyExistException(string message)
       : base(message)
        { }
    }

    public class UncorrectFormatOfCar : Exception
    {
        public UncorrectFormatOfCar(string message)
       : base(message)
        { }
    }

    public class FullParkingSpaces : Exception
    {
        public FullParkingSpaces(string message)
       : base(message)
        { }
    }
}
