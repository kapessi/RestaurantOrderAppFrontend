using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestaurantOrder.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RestaurantOrder.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get([FromQuery] Order order)
        {
            try
            {
                var url = $"https://localhost:44387/api/order/get?DayTime={order.DayTime}";
                order.DishTypes.Split(',').ToList().ForEach(d => url += $"&DishType={d}");
                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.GetAsync(url).Result)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        logger.LogInformation("order/get", order);
                        return JsonConvert.DeserializeObject<IEnumerable<string>>(result);
                    }
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, order);
                return new List<string> { exception.Message };
            }
        }
    }
}