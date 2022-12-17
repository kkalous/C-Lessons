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

        //Todo: Write much more unit tests to prove/disprove David's theory the code is wrong
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

            var cumulativePnls = CalculateCumulativePnlByStrategy(pnlsList);

            var dates = pnlsList
               .OrderBy(o => o.Date)
               .GroupBy(g => g.StrategyId)
               .ToList();

            foreach (var pnl in cumulativePnls)
            {
                var firstDayOfMonth = new DateTime(pnl.Date.Value.Year, pnl.Date.Value.Month, 1);

                var cumulativePnlStartMonth = cumulativePnls.Where(w => w.Date.Value == firstDayOfMonth).Select(s => s.Amount).SingleOrDefault();
                var compoundPnl = pnl.Amount.Value / cumulativePnlStartMonth;

                var newCompoundPnl = new CompoundPnl()
                {
                    Date = pnl.Date.Value,
                    Strategy = pnl.Strategy,
                    Amount = compoundPnl
                };

                results.Add(newCompoundPnl);
            }

            return results;
        }

        public List<CumulativePnl> CalculateCumulativePnlByStrategy(List<Pnl> pnlsList)
        {
            var results = new List<CumulativePnl>();

            var dates = pnlsList
                .OrderBy(o => o.Date)
                .ToList();


            foreach (var date in dates)
            {
                var rslt = pnlsList.Where(w => w.Date <= date.Date).ToList();

                var sumPnls = rslt.Select(s => s.Amount).Sum();

                var cumulativePnl = new CumulativePnl
                {
                    Strategy = pnlsList.Select(s => s.Strategy.StrategyName).First(),
                    Date = date.Date,
                    Amount = sumPnls
                };

                results.Add(cumulativePnl);
            }

            return results;
        }

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
