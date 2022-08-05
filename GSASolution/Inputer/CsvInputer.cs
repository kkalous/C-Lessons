using Data.Scaffolded;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSASolution
{
    public class CsvInputer : IDataInputer
    {
        public IEnumerable<Strategy> ReadStrategies(string fileLoc)
        {
            var lines = ReadLines(fileLoc);
            return ParseStrategies(lines);
        }

        public IEnumerable<string> ReadLines(string fileLoc)
        {
            string[] lines = System.IO.File.ReadAllLines(fileLoc);
            var cleaned = lines.Where(x => x.Contains(","));
            return cleaned;
        }

        public IEnumerable<Strategy> ParseStrategies(IEnumerable<string> lines)
        {
            foreach (var line in lines.Skip(1))
            {
                var split = line.Split(',');
                if (split.Length != 2)
                    throw new Exception($"Cannot parse line {line}. Expected 2 columns but {split.Length} columns");

                var name = split[0].Trim();
                var region = split[1].Trim();

                yield return new Strategy() { StrategyName = name, Region = region };
            }
        }


        public Dictionary<string, List<Pnl>> ReadPnl(string fileLoc)
        {
            var lines = ReadLines(fileLoc);
            return ParsePnl(lines.ToList());
        }

        public Dictionary<string, List<Capital>> ReadCapital(string fileLoc)
        {
            var lines = ReadLines(fileLoc);
            return ParseCapital(lines.ToList());
        }

        private Dictionary<string, List<Pnl>> ParsePnl(List<string> lines)
        {
            return Read(lines, (date, amount) => new Pnl() { Date = date, Amount = amount });
        }
        private Dictionary<string, List<Capital>> ParseCapital(List<string> lines)
        {
            return Read(lines, (date, amount) => new Capital() { Date = date, Amount = amount });
        }

        private Dictionary<string, List<T>> Read<T>(List<string> lines, Func<DateTime, int, T> createFunc)
        {
            var columnHeaders = ColumnHeaders(lines.First()).ToList();

            var columns = new List<List<T>>();

            for (int i = 0; i < columnHeaders.Count(); i++)
            {
                columns.Add(new List<T>());
            }

            foreach (var line in lines.Skip(1))
            {
                var lineCells = line.Split(',');
                var date = DateTime.Parse(lineCells.First());

                for (var i = 1; i < lineCells.Length; i++)
                {
                    columns[i - 1].Add(createFunc(date, int.Parse(lineCells[i])));
                }
            }

            return Enumerable.Range(0, columnHeaders.Count)
                .ToDictionary(x => columnHeaders[x], y => columns[y]);
        }

        private static IEnumerable<string> ColumnHeaders(string headerline)
        {
            var headerSpilt = headerline.Split(',');
            if (!("Date".Equals(headerSpilt[0].Trim(), StringComparison.CurrentCultureIgnoreCase)) || headerSpilt.Length < 2)
                throw new Exception("Header not in correct format");
            return headerSpilt.Skip(1);
        }
    }
}
