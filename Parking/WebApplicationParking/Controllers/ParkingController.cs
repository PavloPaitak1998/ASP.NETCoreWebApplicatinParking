using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApplicationParking.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ParkingController : Controller
    {
        Parking parking;

        public ParkingController(Parking parking) => this.parking = parking;

        [HttpPost]
        public Object AddCar([FromBody]Car car)
        {
            string answer;

            try
            {
                if (car == null||car.Id <= 0 || car.TypeCar == CarType.Undefined)
                {
                    throw new UncorrectFormatOfCar("Uncorrect format of the Car. Please input another car information");
                }
                parking.AddCar(car);
            }
            catch (UncorrectFormatOfCar e)
            {
                answer = JsonConvert.SerializeObject(new { e.Message });
                return JsonConvert.DeserializeObject<Object>(answer);
            }
            catch (CarAlreadyExistException e)
            {
                answer = JsonConvert.SerializeObject(new { e.Message });
                return JsonConvert.DeserializeObject<Object>(answer);
            }
            catch (FullParkingSpaces e)
            {
                answer = JsonConvert.SerializeObject(new { e.Message });
                return JsonConvert.DeserializeObject<Object>(answer);
            }

            answer = JsonConvert.SerializeObject(new { CarInfo = car, Message = "Car is Added" });
            return JsonConvert.DeserializeObject<Object>(answer);
        }

        [HttpDelete("{id}")]
        public Object RemoveCar(int id)
        {
            string answer;

            var car = parking.Cars.Find(c => c.Id == id);
            try
            {
                parking.RemoveCar(car);
            }
            catch (OutOfBalanceException e)
            {
                answer = JsonConvert.SerializeObject(new { CarInfo = car, e.Message });
                return JsonConvert.DeserializeObject<Object>(answer);
            }
            catch (NullReferenceException)
            {
                answer = JsonConvert.SerializeObject(new { Message = "This Car doesn't exist, please input another Car Id!" });
                return JsonConvert.DeserializeObject<Object>(answer);
            }

            answer = JsonConvert.SerializeObject(new { CarInfo = car, Message = "Car is Removed" });
            return JsonConvert.DeserializeObject<Object>(answer);
        }

        [HttpPut("{id}")]
        public Object RefillBalance(int id, [FromBody]string _balance)
        {
            string answer;
            ICar car;

            try
            {
                double balance = double.Parse(_balance);
                if (balance < 0)
                    throw new FormatException();

                car = parking.Cars.Find(c => c.Id == id);

                parking.RefillBalance(car, balance);
            }
            catch (NullReferenceException)
            {
                answer = JsonConvert.SerializeObject(new { Message = "This Car doesn't exist, please input another Car Id!" });
                return JsonConvert.DeserializeObject<Object>(answer);
            }
            catch (FormatException)
            {
                answer = JsonConvert.SerializeObject(new { Message = "Uncorrect format of data, please input again!" });
                return JsonConvert.DeserializeObject<Object>(answer);
            }
            catch (ArgumentNullException)
            {
                answer = JsonConvert.SerializeObject(new { Message = "Uncorrect format of data, please input again!" });
                return JsonConvert.DeserializeObject<Object>(answer);
            }

            answer = JsonConvert.SerializeObject(new { CarInfo = (Car)car, Message = "Car balance is Refilled" });
            return JsonConvert.DeserializeObject<Object>(answer);
        }

        [HttpGet]
        public List<Car> CarList()
        {
            string answer = JsonConvert.SerializeObject(parking.Cars);

            return JsonConvert.DeserializeObject<List<Car>>(answer);
        }

        [HttpGet("{id}")]
        public Object CarInfo(int id)
        {
            ICar car;
            string answer;

            try
            {
                car = parking.Cars.Find(c => c.Id == id);

                if (car == null)
                {
                    throw new CarNotExistException("This Car doesn't exist, please input another Car Id!");
                }
            }
            catch (CarNotExistException e)
            {
                answer = JsonConvert.SerializeObject(new { e.Message });
                return JsonConvert.DeserializeObject<Object>(answer);
            }

            answer = JsonConvert.SerializeObject(new { CarInfo = (Car)car, Message = "Information about Car" });
            return JsonConvert.DeserializeObject<Object>(answer);
        }

        [HttpGet]
        public Object FreeParkingSpaces()
        {
            string str = JsonConvert.SerializeObject(new { parking.FreeParkingSpace });
            return JsonConvert.DeserializeObject<Object>(str);
        }

        [HttpGet]
        public Object BusyParkingSpaces()
        {
            string answer = JsonConvert.SerializeObject(new { parking.BusyParkingSpace });
            return JsonConvert.DeserializeObject<Object>(answer);
        }

        [HttpGet]
        public Object ParkingSpaces()
        {
            string answer = JsonConvert.SerializeObject(new { Settings.ParkingSpace });
            return JsonConvert.DeserializeObject<Object>(answer);
        }

        [HttpGet]
        public Object Balance()
        {
            string answer = JsonConvert.SerializeObject(new { parking.Balance });
            return JsonConvert.DeserializeObject<Object>(answer);
        }

    }
}