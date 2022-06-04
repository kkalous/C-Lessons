using System;
using System.Collections.Generic;

namespace ExtensionMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var year = GetYear();

            if (year == 0)
            {
                Console.WriteLine("Year inserted is in wrong format: ");
                year = GetYear();


            } 

            var century = new ExtensionExample
            {
                Year = year
            };

            Console.WriteLine($"The Century is: {Extensions.GetCentury(century)}");
        }

        private static int GetYear()
        {
            Console.WriteLine("Insert Year: ");
            var insertedYear = Console.ReadLine();
            int.TryParse(insertedYear, out int yearNumber);

            return yearNumber;
        }
    }
}
