using Data.Scaffolded;
using System.Linq;

namespace GSASolution
{
    public class DatabaseSeeder
    {
        private string _inputFilesDirectory;
        private readonly IDataInputer dataInputer;

        public DatabaseSeeder(string inputFilesDirectory, IDataInputer dataInputer)
        {
            this._inputFilesDirectory = inputFilesDirectory;
            this.dataInputer = dataInputer;
        }

        public void SeedStrategies()
        {
            var strategies = dataInputer.ReadStrategies(_inputFilesDirectory + @"\properties.csv").ToList();
            var pnl = dataInputer.ReadPnl(_inputFilesDirectory + @"\pnl.csv");
            var capital = dataInputer.ReadCapital(_inputFilesDirectory + @"\capital.csv");

            foreach (var strategy in strategies)
            {
                if (capital.ContainsKey(strategy.StrategyName))
                    strategy.Capitals = capital[strategy.StrategyName];

                if (pnl.ContainsKey(strategy.StrategyName))
                    strategy.Pnls = pnl[strategy.StrategyName];
            }

            using (var context = new GsaContext())
            {
                context.Seed(strategies);
            }
        }
    }
}
