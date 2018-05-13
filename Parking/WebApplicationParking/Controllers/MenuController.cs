using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplicationParking.Models;


namespace WebApplicationParking.Controllers
{
    [Produces("application/json")]
    [Route("api/Menu")]
    public class MenuController : Controller
    {
        [HttpGet]
        public MenuInfo Get()
        {
            MenuInfo menu = new MenuInfo();

            string str = JsonConvert.SerializeObject(menu);

            return JsonConvert.DeserializeObject<MenuInfo>(str);
        }
    }
}