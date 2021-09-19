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

        private readonly ILogger<IceCreamController> _logger;

        public IceCreamController(ILogger<IceCreamController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public object Get()
        {
            var prices = new PriceRepository().Get();

            return Ok(prices);
        }
    }
}
