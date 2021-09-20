using IceCream.VMs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace IceCream.DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private const string OrdersPath = "JSON/orders.json";
        private static string[] Sizes = { "small", "medium", "large" };
        private List<PriceVM> Prices;
        private readonly IPriceRepository _priceRepo;

        public OrderRepository(IPriceRepository priceRepo)
        {
            _priceRepo = priceRepo;
            Prices = _priceRepo.Get();
        }
        public void Generate()
        {
            var _data = new List<OrderVM>();

            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int randomUser = random.Next(1, 101);
                int randomProduct = random.Next(0, Prices.Count);
                int randomSize = random.Next(0, Sizes.Length);

                _data.Add(new OrderVM()
                {
                    product = Prices[randomProduct].product,
                    size = Sizes[randomSize],
                    user = "Ahmed" + randomUser
                });
            }


            string json = System.Text.Json.JsonSerializer.Serialize(_data);
            File.WriteAllText(OrdersPath, json);
        }

        public List<OrderVM> Get()
        {
            var jsonText = File.ReadAllText(OrdersPath);
            var orders = JsonConvert.DeserializeObject<List<OrderVM>>(jsonText);

            return orders;
        }

        public double GetAmount(OrderVM order)
        {
            var productPrices = Prices.FirstOrDefault(c => c.product == order.product).prices;
            if (order.size == "small")
                return productPrices.small;
            if (order.size == "medium")
                return productPrices.medium;
            return productPrices.large;
        }
    }
}
