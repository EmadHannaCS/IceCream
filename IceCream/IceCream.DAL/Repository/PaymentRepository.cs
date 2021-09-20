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
    public class PaymentRepository : IPaymentRepository
    {
        private const string PaymentsPath = "JSON/payments.json";
        private readonly IOrderRepository _orderRepo;
        private readonly IPriceRepository _priceRepo;


        public PaymentRepository(IOrderRepository orderRepo, IPriceRepository priceRepo)
        {
            _orderRepo = orderRepo;
            _priceRepo = priceRepo;
        }
        public void Generate()
        {
            var userRandomDic = new Dictionary<string, int>();//to make some users pay always per purchase to make some balances to be zeros
            var _data = new List<PaymentVM>();

            var orders = _orderRepo.Get();
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int randomAmount = random.Next(1, 5);
                if (userRandomDic.ContainsKey(orders[i].user))
                    randomAmount = userRandomDic[orders[i].user];
                else
                    userRandomDic.Add(orders[i].user, randomAmount);

                _data.Add(new PaymentVM()
                {
                    user = orders[i].user,
                    amount = _orderRepo.GetAmount(orders[i]) * randomAmount
                });
            }


            string json = System.Text.Json.JsonSerializer.Serialize(_data);
            File.WriteAllText(PaymentsPath, json);
        }

        public List<PaymentVM> Get()
        {
            var jsonText = File.ReadAllText(PaymentsPath);
            var payments = JsonConvert.DeserializeObject<List<PaymentVM>>(jsonText);

            return payments;
        }


    }
}
