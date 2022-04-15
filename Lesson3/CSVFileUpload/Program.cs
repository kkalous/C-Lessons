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
            //var strategyPln = new StrategyPnl();
            var lines = File.ReadAllLines("C:\\Users\\Kamila\\Desktop\\C# projects\\CSharpJames\\Gsa\\pnl.csv");
            //.Split(",");

            List<string[]> data = new List<string[]>();

            foreach (var line in lines)
            {
                var vals = line.Split(",");
                data.Add(vals);
            }

            //List<string> strategyNames = new List<string>();

            //for (int i = 1; i < 16; i++ )
            //{
            //    var strategyName = data.First()[i];
            //    strategyNames.Add(strategyName);
            //}

        /*    List<DateTime> dates = new List<DateTime>();
            for (int i = 1; i < lines.Length; i++)
            {
                var date = DateTime.Parse(data[i].First());
                dates.Add(date);
            }*/

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
