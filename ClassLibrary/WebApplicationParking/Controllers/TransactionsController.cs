using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationParking.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TransactionsController : Controller
    {
        Parking parking;

        public TransactionsController(Parking parking) => this.parking = parking;

    }
}