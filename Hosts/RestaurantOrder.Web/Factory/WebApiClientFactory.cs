using Microsoft.Extensions.Configuration;
using RestaurantOrder.Web.Models;
using System.Linq;

namespace RestaurantOrder.Web.Factory
{
    public class WebApiClientFactory
    {
        public string CreateUri(IConfiguration configuration, Order order)
        {
            var section = configuration.GetSection("AppSettings");
            var webApiUrl = section["WebApiUrl"];
            webApiUrl = $"{webApiUrl}?DayTime={order.DayTime}";
            order.DishTypes.Split(',').ToList().ForEach(d => webApiUrl += $"&DishType={d}");
            return webApiUrl;
        }
    }
}