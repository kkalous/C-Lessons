using Data;
using Data.Scaffolded;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSASolution.Outputer
{
    public class CsvOutputer
    {
        private readonly ICumulativePnlService _cumulativePnlService;
        public CsvOutputer(ICumulativePnlService cumulativePnlService)
        {
            _cumulativePnlService = cumulativePnlService;
        }

        public List<Capital> GetCapitalsInfo(string strategies)
        {
            var strategiesList = strategies.Split(",").ToList();

            using var db = new GsaContext();

            var strategyInfo = GetStrategyInfo(strategiesList, db);

            //Get Capital
            var results = new List<Capital>();
            foreach (var strategy in strategyInfo)
            {
                var capitalsList = db.Capitals
                                        .Include(i => i.Strategy)
                                        .Where(w => w.StrategyId == strategy.StrategyId);
                foreach (var capital in capitalsList)
                {
                    results.Add(capital);
                }
            }

            return results;

        }

        public List<CumulativePnl> GetCumulativePnlsInfo(string region)
        {
            using var db = new GsaContext();

            var trimedRegion = region.Trim().TrimEnd();

            var results = new List<CumulativePnl>();

            //Calculate Cumulative Pnl 
            var pnlsList = db.Pnls
                            .Include(i => i.Strategy)
                            .Where(w => w.Strategy.Region == trimedRegion).ToList();

            var rslt = _cumulativePnlService.CalculateCumulativePnl(pnlsList);

            results.AddRange(rslt);


            return results;
        }
        private List<Strategy> GetStrategyInfo(List<string> strategiesList, GsaContext db)
        {
            var results = new List<Strategy>();


            //Get strategy id, region
            foreach (var strategy in strategiesList)
            {
                var trimedStrategy = strategy.TrimEnd().Trim();
                var str = db.Strategies.Where(w => w.StrategyName.ToUpper() == trimedStrategy.ToUpper()).FirstOrDefault();
                results.Add(str);
            }

            return results;


        }
    }
}
