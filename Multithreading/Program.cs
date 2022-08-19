using System;

namespace Multhitreeading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var allNames = new NamesService();

            var _matchingService = new MatchingService();

            var retunrnedMatches = _matchingService.GetNameMatches(allNames.names);


            foreach (var match in retunrnedMatches)
            {
                Console.WriteLine(match.ToString());
            }
        }
    }
}
