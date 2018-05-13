using System;

namespace ClassLibrary
{
    public interface ICar
    {
        int Id { get; set; }
        double Balance { get; set; }
        CarType TypeCar { get; set; }
    }
}
