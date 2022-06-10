using Checkout.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Checkout
{
    public interface IProductDb
    {
        Dictionary<string, Product> GetProducts();
    }
    public class ProductDb : IProductDb
    {        
        public Dictionary<string, Product> GetProducts()
        {

            var lines = File.ReadAllLines("C:\\Users\\Kamila\\Desktop\\C# projects\\C-Lessons\\Checkout\\Product.csv");

            var data = lines.Skip(1);

            var dictionary = new Dictionary<string, Product>();

            foreach (var line in data)
            {
                var cell = line.Split(",");
                var productName = cell[0].ToString();
                    var product = new Product()
                   {
                        Name = cell[0],
                        Price = decimal.Parse(cell[1]),
                        Weight = decimal.Parse(cell[2]),
                        Type = cell[3]                      
                   };

                    dictionary.Add(productName, product);               
            }

            return dictionary;
        }
    }

}
