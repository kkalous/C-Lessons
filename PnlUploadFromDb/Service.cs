//using PnlUploadFromDb.Scaffolded;
using PnlUploadFromDb.Scaffolded;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PnlUploadFromDb
{
    public class Service
    {
        public void SavePnlCapital(List<PnlCapital> pnls, List<PnlCapital> capitals)
        {
            using (var db = new CLessonsContext())
            {
                foreach(var pnl in pnls)
                {
                    db.PnlCapitals.Add(pnl);
                    db.SaveChanges();
                }

                foreach (var capital in capitals)
                {
                    db.PnlCapitals.Add(capital);
                    db.SaveChanges();
                }

            }
                
        }

        public List<PnlCapital> GetPnlCapital(string amountType, string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            var header = lines[0].Split(",").ToArray();
            var data = lines.Skip(1);

            var strategyDict = GetStrategies();
            var results = new List<PnlCapital>();

            foreach (var line in data)
            {
                var cell = line.Split(",");
                var date = Convert.ToDateTime(cell[0]);
                for (int i = 1; i < cell.Length; i++)
                {                   
                    var capital = new PnlCapital()
                    {
                        AmountType = amountType,
                        StrategyId = strategyDict[header[i]],
                        Amount = Convert.ToDecimal(cell[i]),
                        Date = date
                    };

                    results.Add(capital);
                }
            }

            return results;
        }

        public Dictionary<string, int> GetStrategies()
        {
            var strategyDict = new Dictionary<string, int>();

            List<Strategy> query;
            using (var db = new CLessonsContext())
            {
               query = db.Strategies.ToList();
            }

            foreach( var strat in query)
            {
                strategyDict.Add(strat.StrategyName, strat.StrategyId);
            }

            return strategyDict;           

        }

    }
}

