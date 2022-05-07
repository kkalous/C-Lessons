using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSVFileUpload
{
    class Program
    {
        static void Main(string[] args)
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

            //List<string[]> data = new List<string[]>();

            /*foreach (var line in lines)
            {
                var vals = line.Split(",");
                data.Add(vals);
            }

            List<StrategyPnl> strategyPnl = new List<StrategyPnl>();
            for (int i = 1; i < 16; i++)
            {
                for (var y = 1; y < data.Count; y++)
                {
                    var strategyList = new StrategyPnl()
                    {
                        //Strategy = strategyNames[i],
                        Strategy = data.First()[i],
                        Pnls = new List<Pnl>()
                        {
                            new Pnl
                            {
                                //Date = dates[y],
                                Date = DateTime.Parse(data[i].First()),
                                Amount = decimal.Parse(data[y].GetValue(i).ToString())
                            }
                        }
                    };

                    strategyPnl.Add(strategyList);
                }
            }            
       
        }*/


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
