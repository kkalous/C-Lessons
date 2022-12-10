using Data.Scaffolded;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSASolution.Outputer
{
    // Bad class name
    public class CsvOutputer
    {
        private readonly IPnlService _cumulativePnlService;
        public CsvOutputer(IPnlService cumulativePnlService)
        {
            _cumulativePnlService = cumulativePnlService;
        }

        public List<Capital> GetCapitalsInfo(string strategies)
        {
            var strategiesList = strategies.Split(",").ToList();

            using var db = new GsaContext();

            var strategyInfo = GetStrategyInfo(strategiesList, db);

            //Get Capital
            // TODO: Do this all using one db call to fetch data
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

        public List<CumulativePnl> GetCumulativePnlsInfo(string region, string strategy = null)
        {
            using var db = new GsaContext();

            var trimedRegion = region.Trim().TrimEnd();

            var results = new List<CumulativePnl>();

            var pnlsList = new List<Pnl>();

            if (strategy != null)
            {
                //Calculate Cumulative Pnl 
                pnlsList = db.Pnls
                                .Include(i => i.Strategy)
                                .Where(w => w.Strategy.StrategyName.ToUpper() == strategy.ToUpper()).ToList();
            }
            else
            {
                //Calculate Cumulative Pnl 
                pnlsList = db.Pnls
                                .Include(i => i.Strategy)
                                .Where(w => w.Strategy.Region == trimedRegion).ToList();
            }

            var rslt = _cumulativePnlService.CalculateCumulativePnlByRegion(pnlsList);

            results.AddRange(rslt);


            return results;
        }

        public List<CumulativePnl> GetCumulativePnlsInfoWithStartingDate(string region, DateTime? startDate = null)
        {
            using var db = new GsaContext();

            var trimedRegion = region.Trim().TrimEnd();

            var results = new List<CumulativePnl>();

            //Calculate Cumulative Pnl 
            var pnlsList = db.Pnls
                            .Include(i => i.Strategy)
                            .Where(w => w.Strategy.Region == trimedRegion
                            & (w.Date >= startDate || startDate == null))
                            .ToList();

            var rslt = _cumulativePnlService.CalculateCumulativePnlByRegion(pnlsList);

            results.AddRange(rslt);


            return results;
        }


        private List<Strategy> GetStrategyInfo(List<string> strategiesList, GsaContext db)
        {
            //Get strategy id, region
            var trimedStrategies = strategiesList.Select(x => x.TrimEnd().Trim().ToUpper());

            var results = db.Strategies.Where(w => trimedStrategies.Contains(w.StrategyName.ToUpper())).ToList();
            return results;
        }

        public List<CompoundPnl> CalculateCompoundPnl(string strategy)
        {
            var results = new List<CompoundPnl>();

            return results;
        }
    }
}
