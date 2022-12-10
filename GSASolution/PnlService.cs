using Data.Scaffolded;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSASolution
{
    public interface IPnlService
    {
        List<CumulativePnl> CalculateCumulativePnlByRegion(List<Pnl> pnlsList);
        List<CompoundPnl> CalculateCompoundPnl(List<Pnl> pnlsList);
    }


    public class PnlService : IPnlService
    {      

        public List<CumulativePnl> CalculateCumulativePnlByRegion(List<Pnl> pnlsList)
        {
            var results = new List<CumulativePnl>();

            var pnls = pnlsList
                .GroupBy(g => g.Date)
                .OrderBy(o => o.Key)
                .ToList();

            var total = 0m;

            foreach (var pnlGroup in pnls)
            {
                var pnlForDate = pnlGroup.ToList().Sum(x => x.Amount);

                total += pnlForDate.Value;               

                var cumulativePnl = new CumulativePnl
                {
                    Region = pnlsList.Select(s => s.Strategy.Region).First(),
                    Date = pnlGroup.Key,
                    Amount = total
                };

                results.Add(cumulativePnl);
            }

            return results;
        }
      

        public List<CompoundPnl> CalculateCompoundPnl(List<Pnl> pnlsList)
        {
            var results = new List<CompoundPnl>();

            var cumulativePnls = CalculateCumulativePnlByRegion(pnlsList);

            foreach (var pnl in cumulativePnls)
            {

            }

            return results;
        }

        //public List<CumulativePnl> CalculateCumulativePnl(List<Pnl> pnlsList)
        //{
        //    var results = new List<CumulativePnl>();

        //    var dates = pnlsList
        //        .OrderBy(o => o.Date)
        //        .ToList();


        //    foreach (var date in dates)
        //    {
        //        var rslt = pnlsList.Where(w => w.Date <= date.Date).ToList();

        //        var sumPnls = rslt.Select(s => s.Amount).Sum();

        //        var cumulativePnl = new CumulativePnl
        //        {
        //            Region = pnlsList.Select(s => s.Strategy.Region).First(),
        //            Date = date.Date,
        //            Amount = sumPnls
        //        };

        //        results.Add(cumulativePnl);
        //    }

        //    return results;
        //}

    }

    public class CumulativePnl
    {
        public string Region { get; set; }
        public string Strategy { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
    }

    public class CompoundPnl
    {
        public string Strategy { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
    }
}
