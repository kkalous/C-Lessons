using Checkout.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkout
{
    public class CheckoutService
    {
        private readonly IProductDb _products;
        public CheckoutService(IProductDb products)
        {
            _products = products;
        }

        public Product TotalCheckOut(string purchasedProducts)
        {
            var splitPurhasedProducts = GetPurchasedProducts(purchasedProducts);        

            //Get Total Price
            var totalPrice = GetProductsPrice(splitPurhasedProducts);

            //Calculate Total weight
            var totalWeight = GetProductsWeight(splitPurhasedProducts);

            //Categories 
            var allCategories = GetProductsCategories(splitPurhasedProducts);

            var productsInfo = new Product
            {
                Price = totalPrice,
                Weight = totalWeight,
                Type = allCategories
            };

            return productsInfo;
        }

        private List<string> GetPurchasedProducts(string purchasedProducts)
        {
            var splitPurhasedProducts = new List<string>();

            for (int i = 0; i < purchasedProducts.Length; i++)
            {
                var oneProduct = purchasedProducts[i].ToString();
                splitPurhasedProducts.Add(oneProduct);
            }

            return splitPurhasedProducts;
        }

        private decimal GetProductsPrice(List<string> purchasedProductList)
        {
            var pricesList = new List<decimal>();

            var allProducts = _products.GetProducts();

            foreach (var product in purchasedProductList)
            {
                allProducts.TryGetValue(product, out Model.Product productOutput);
                var price = productOutput.Price;

                pricesList.Add(price);
            }

            //Calculate Total Price
            decimal total = pricesList.Sum();

            //Calculate discount if allowed
            var discount = Discount(purchasedProductList, total);
            total -= discount;

            return total;
        }       

        private decimal Discount(List<string> purchasedProducts, decimal total)
        {
            decimal totalDiscount = 0;

            //Discount if more then 10 item is bought
            if (purchasedProducts.Count() > 10)
            {
                var tenDiscount = total / 10;
                totalDiscount += tenDiscount;
            }

            //Discount for B - buy one, get one free
            var numberOfBProducts = purchasedProducts.Where(x => x == "B").Count();
            if (numberOfBProducts > 1)
            {
                var getBProduct = _products.GetProducts().TryGetValue("B", out Model.Product productOutput);
                var price = productOutput.Price;
                int calc = numberOfBProducts / 2;
                decimal bDiscount = calc * price; 
                totalDiscount += bDiscount;
            }

            //Discount for 3 Ds
            var numberOfDProducts = purchasedProducts.Where(x => x == "D").Count();
            if (numberOfDProducts > 2)
            {
                var getDProduct = _products.GetProducts().TryGetValue("D", out Model.Product productOutput);
                var price = productOutput.Price;
                int calc = numberOfDProducts / 3;
                decimal dDiscount = calc * price; 
                totalDiscount += dDiscount;
            }

            return totalDiscount;
        }

        private decimal GetProductsWeight(List<string> purchasedProductList)
        {
            var weightList = new List<decimal>();

            var allProducts = _products.GetProducts();

            foreach (var product in purchasedProductList)
            {
                allProducts.TryGetValue(product, out Model.Product productOutput);
                var weight = productOutput.Weight;

                weightList.Add(weight);
            }

            //Calculate Total weight
            decimal total = weightList.Sum();

            return total;
        }

        private string GetProductsCategories(List<string> purchasedProductList)
        {
            var categoriesList = new List<string>();

            var allProducts = _products.GetProducts();

            foreach (var product in purchasedProductList)
            {
                allProducts.TryGetValue(product, out Model.Product productOutput);
                var category = productOutput.Type;

                categoriesList.Add(category);
            }

            //Calculate Total weight
            var allCategories = String.Join(", ", categoriesList.Distinct().ToArray());

            return allCategories;
        }
    }
}
