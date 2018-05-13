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
    public class TransactionsController : Controller
    {
        Parking parking;

        public TransactionsController(Parking parking) => this.parking = parking;

        [HttpGet]
        public List<Transaction> TransactionHistory()
        {
            DateTime date = DateTime.Now;

            var transact = parking.Transactions.GetRange(0, parking.Transactions.Count);

            var transactions = transact.Where(t => (date - t.TransactionTime).TotalSeconds <= 60);

            string str = JsonConvert.SerializeObject(transactions);
            return JsonConvert.DeserializeObject<List<Transaction>>(str);
        }

    }
}