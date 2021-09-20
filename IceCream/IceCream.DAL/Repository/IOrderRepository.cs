using IceCream.VMs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IceCream.DAL.Repository
{
    public interface IOrderRepository
    {
        List<OrderVM> Get();

        void Generate();

        double GetAmount(OrderVM order);
    }
}
