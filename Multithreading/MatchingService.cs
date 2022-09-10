using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multhitreeading
{
    public class MatchingService
    {
        public List<MatchModel> GetNameMatches(string[] names)
        {
            var namesList = new List<MatchModel>();

            Parallel.ForEach(names, (name, state) =>
            {

                var listWithAllRatios = new List<MatchModel>();

                for (int l = 0; l < names.Length; l++)
                {
                    var name1 = name;
                    var name2 = names[l];

                    var ratio = FuzzySharp.Levenshtein.GetRatio(name1, name2);
                    if (ratio != 1 & ratio != 0)
                    {
                        var match = new MatchModel()
                        {
                            Name1 = name1,
                            Name2 = name2,
                            Ratio = Math.Round(ratio, 2)
                        };

                        listWithAllRatios.Add(match);
                    }
                    continue;
                }

                var highestRatio = listWithAllRatios.OrderByDescending(o => o.Ratio).FirstOrDefault();
                if (highestRatio != null)
                     namesList.Add(highestRatio);

            }
            );
            var results = namesList;

            return results;

        }
    }
}
