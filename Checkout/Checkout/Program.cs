using System;

namespace Checkout
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CheckoutService checkoutService = new CheckoutService(new ProductDb());

            Console.WriteLine("Insert Products");

            var products = Console.ReadLine();
            var result = checkoutService.TotalCheckOut(products);

            Console.WriteLine($"Total is: {result.Price}");    
            Console.WriteLine($"Weight is: {result.Weight}");
            Console.WriteLine($"Categories: {result.Type}");

        }
    }
}
