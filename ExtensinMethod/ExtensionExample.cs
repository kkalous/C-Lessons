using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethod
{
    public class ExtensionExample
    {
        public int Year { get; set; }

    }
    public static class Extensions
    {
        public static int GetCentury(this ExtensionExample year)
        {
            var yr = year.Year.ToString();
            var firstTwoDigits = int.Parse(yr.Substring(0,2).ToString());
            var century = firstTwoDigits + 1;

            return century;
        }
    }
}
