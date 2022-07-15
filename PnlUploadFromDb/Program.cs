using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using PnlData.Scaffolded;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PnlUploadFromDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write 'I' for inserting data into Db or 'L' for loading data from Db");
            var option = Console.ReadLine();

            if (option == "L")
            {
                var localConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

                List<PnlData.Scaffolded.Pnl> query;
                using (var db = new Context())
                {
                    query = db.Pnls
                        .OrderBy(x => x.Date)
                        .ToList();
                }

                Console.WriteLine($"Loaded {query.Count()} rows");
            }

            else if (option == "I")
            {
                Console.WriteLine("\n");

                var lines = File.ReadAllLines("C:\\Users\\Kamila\\Desktop\\C# projects\\CSharpJames\\Gsa\\pnl.csv");

                var header = lines[0].Split(",").Skip(1).ToArray();
                var data = lines.Skip(1);

                var dictionary = new Dictionary<string, List<Pnl>>();

                foreach (var strategy in header)
                {
                    dictionary.Add(strategy, new List<Pnl>());
                }

                foreach (var line in data)
                {
                    var cell = line.Split(",");
                    var date = Convert.ToDateTime(cell[0]);
                    for (int i = 1; i < cell.Length; i++)
                    {
                        var pnl = new Pnl()
                        {
                            Amount = Convert.ToDecimal(cell[i]),
                            Date = date
                        };

                        var strategy = header[i - 1];
                        dictionary[strategy].Add(pnl);
                    }
                }

                using (var db = new Context())
                 {
                    foreach (var strategy in dictionary)
                    {
                        var strategyName = strategy.Key;
                        List<Pnl> list = strategy.Value;

                        foreach (var amount in list)
                        {
                            var newPnl = new PnlData.Scaffolded.PnlInsert()
                            {
                                Date = amount.Date,
                                StrategyName = strategyName,
                                Amount = amount.Amount
                            };

                            db.PnlInserts.Add(newPnl);
                        } 
                        
                    }

                    db.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("Don't recognize this command");
            }
        }
    }

    public class StrategyPnl
    {
        public string Strategy { get; set; }
        public List<Pnl> Pnls { get; set; }
    }

    public class Pnl
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}

  


