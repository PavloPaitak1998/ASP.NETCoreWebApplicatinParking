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
            string str;

            try
            {
                if (car.Id <= 0 || car.TypeCar == CarType.Undefined)
                {
                    throw new UncorrectFormatOfCar("Uncorrect format of the Car. Please input another car information");
                }
                parking.AddCar(car);
            }
            catch (UncorrectFormatOfCar e)
            {
                str = JsonConvert.SerializeObject(new { e.Message });
                return JsonConvert.DeserializeObject<Object>(str);
            }
            catch (CarAlreadyExistException e)
            {
                str = JsonConvert.SerializeObject(new { e.Message });
                return JsonConvert.DeserializeObject<Object>(str);
            }
            catch (FullParkingSpaces e)
            {
                str = JsonConvert.SerializeObject(new { e.Message });
                return JsonConvert.DeserializeObject<Object>(str);
            }

            str = JsonConvert.SerializeObject(new { CarInfo = car, Message = "Car is Added" });
            return JsonConvert.DeserializeObject<Object>(str);
        }

        [HttpDelete("{id}")]
        public Object RemoveCar(int id)
        {
            string str;

            var car = parking.Cars.Find(c => c.Id == id);
            try
            {
                parking.RemoveCar(car);
            }
            catch (OutOfBalanceException e)
            {
                str = JsonConvert.SerializeObject(new { CarInfo = car, e.Message });
                return JsonConvert.DeserializeObject<Object>(str);
            }
            catch (NullReferenceException)
            {
                str = JsonConvert.SerializeObject(new { Message = "This Car doesn't exist, please input another Car Id!" });
                return JsonConvert.DeserializeObject<Object>(str);
            }

            str = JsonConvert.SerializeObject(new { CarInfo = car, Message = "Car is Removed" });
            return JsonConvert.DeserializeObject<Object>(str);
        }

        [HttpPut("{id}")]
        public Object RefillBalance(int id, [FromBody]string _balance)
        {
            string str;
            ICar car;

            try
            {
                double balance = double.Parse(_balance);

                car = parking.Cars.Find(c => c.Id == id);

                parking.RefillBalance(car, balance);
            }
            catch (NullReferenceException)
            {
                str = JsonConvert.SerializeObject(new { Message = "This Car doesn't exist, please input another Car Id!" });
                return JsonConvert.DeserializeObject<Object>(str);
            }
            catch (FormatException)
            {
                str = JsonConvert.SerializeObject(new { Message = "Uncorrect format of data, please input again!" });
                return JsonConvert.DeserializeObject<Object>(str);
            }
            catch (ArgumentNullException)
            {
                str = JsonConvert.SerializeObject(new { Message = "Uncorrect format of data, please input again!" });
                return JsonConvert.DeserializeObject<Object>(str);
            }

            str = JsonConvert.SerializeObject(new { CarInfo = (Car)car, Message = "Car balance is Refilled" });
            return JsonConvert.DeserializeObject<Object>(str);
        }

    }
}