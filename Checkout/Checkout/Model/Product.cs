using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Model
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public string Type { get; set; }
    }
}
