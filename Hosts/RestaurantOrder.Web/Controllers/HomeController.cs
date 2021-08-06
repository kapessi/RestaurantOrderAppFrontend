using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestaurantOrder.Web.Factory;
using RestaurantOrder.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RestaurantOrder.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private IConfiguration configuration;
        private readonly ILogger<HomeController> logger;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get([FromQuery] Order order)
        {
            try
            {
                var webApiClientFactory = new WebApiClientFactory();
                var webApiUrl = webApiClientFactory.CreateUri(configuration, order);
                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.GetAsync(webApiUrl).Result)
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