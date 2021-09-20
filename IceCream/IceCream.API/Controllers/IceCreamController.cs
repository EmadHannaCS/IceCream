using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IceCream.DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IceCream.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IceCreamController : ControllerBase
    {

        private readonly IPriceRepository _priceRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IPaymentRepository _paymentRepo;

        public IceCreamController(IPriceRepository priceRepo, IOrderRepository orderRepo, IPaymentRepository paymentRepo)
        {
            _priceRepo = priceRepo;
            _orderRepo = orderRepo;
            _paymentRepo = paymentRepo;
        }

        [HttpGet]
        public object Get()
        {

            _orderRepo.Generate();
            _paymentRepo.Generate();

            var prices = _priceRepo.Get();
            var orders = _orderRepo.Get();
            var payments = _paymentRepo.Get();

            var usersOrdersAmounts = orders.GroupBy(c => c.user).Select(c => new { user = c.Key, TotalOrdersAmount = c.Sum(s => _orderRepo.GetAmount(s)) }).ToList();

            var usersPaymentsAmounts = payments.GroupBy(c => c.user).Select(c => new { user = c.Key, TotalPaymentsAmount = c.Sum(s => s.amount) }).ToList();

            var usersBalances = usersPaymentsAmounts.Select(c => new
            {
                c.user,
                Balance = c.TotalPaymentsAmount - usersOrdersAmounts.FirstOrDefault(u => u.user == c.user).TotalOrdersAmount
            }).ToList();

            return Ok(new { prices, orders, payments, usersOrdersAmounts, usersPaymentsAmounts, usersBalances });
        }
    }
}
