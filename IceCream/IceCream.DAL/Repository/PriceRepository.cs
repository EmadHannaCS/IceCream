using IceCream.VMs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IceCream.DAL.Repository
{
    public class PriceRepository : IPriceRepository
    {
        public List<PriceVM> Get()
        {
            var jsonText = File.ReadAllText("JSON/prices.json");
            var prices = JsonConvert.DeserializeObject<List<PriceVM>>(jsonText);

            return prices;
        }
    }
}
