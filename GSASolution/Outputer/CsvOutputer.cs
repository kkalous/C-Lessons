using Data.Scaffolded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSASolution.Outputer
{
    public class CsvOutputer
    {
        public IEnumerable<string> GetCapitalsInfo(string strategies)
        {
            try
            {
                var strategiesList = strategies.Split(",").ToList();

                using (var db = new GsaContext())
                {
                    var strategyInfo = GetStrategyInfo(strategiesList, db);

                    //Get Capital
                    var results = new List<string>();
                    foreach (var strategy in strategyInfo)
                    {
                        var capitalsList = db.Capitals.Where(w => w.StrategyId == strategy.StrategyId).ToList();
                        foreach (var capital in capitalsList)
                        {
                            var rslt = @$"{strategy.StrategyName}, date:{capital.Date}, capital:{capital.Amount}";
                            results.Add(rslt);
                        }
                    }

                    return results;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wrong Command!");
                throw ex;
            }
        }

        public IEnumerable<string> GetPnlsInfo(string strategies)
        {
            try
            {
                var strategiesList = strategies.Split(",").ToList();

                using (var db = new GsaContext())
                {
                    var strategyInfo = GetStrategyInfo(strategiesList, db);

                    //Get Pnl
                    var results = new List<string>();
                    foreach (var strategy in strategyInfo)
                    {
                        var pnlsList = db.Pnls.Where(w => w.StrategyId == strategy.StrategyId).ToList();
                        foreach (var pnl in pnlsList)
                        {
                            var rslt = @$"{strategy.Region}, date:{pnl.Date}, capital:{pnl.Amount}";
                            results.Add(rslt);
                        }
                    }

                    return results;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wrong Command!");
                throw ex;
            }
        }

        private List<Strategy> GetStrategyInfo(List<string> strategiesList, GsaContext db)
        {
            var strategiesInfo = new List<Strategy>();
            //Get strategy id, region
            foreach (var strategy in strategiesList)
            {
                var trimedStrategy = strategy.TrimEnd().Trim();
                var str = db.Strategies.Where(w => w.StrategyName.ToUpper() == trimedStrategy.ToUpper()).FirstOrDefault();
                strategiesInfo.Add(str);
            }

            return strategiesInfo;
        }
    }
}
