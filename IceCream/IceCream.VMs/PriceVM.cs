using System;
using System.Collections.Generic;
using System.Text;

namespace IceCream.VMs
{
    public class PriceVM
    {
        public string product { get; set; }

        public prices prices { get; set; }
    }

    public class prices
    {
        public double small { get; set; }
        public double medium { get; set; }
        public double large { get; set; }
    }
}
