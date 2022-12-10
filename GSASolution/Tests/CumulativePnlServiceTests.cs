using Data.Scaffolded;
using GSASolution;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tests
{
    public class CumulativePnlServiceTests
    {
        private PnlService _cumulativePnlService;

        [SetUp]
        public void Setup()
        {
            _cumulativePnlService = new PnlService();
        }

        [Test]
        public void CreateCumulativePnlsListPass()
        {
            //Arrange
            var listPnl = new List<Pnl>();

            var pnl = new Pnl()
            {
                StrategyId = 2,
                Date = Convert.ToDateTime("01-01-2022"),
                Amount = 200,
                Strategy = new Strategy()
                {
                    StrategyId = 2,
                    Region = "EU"
                }
            };

            listPnl.Add(pnl);

            var pnl1 = new Pnl()
            {
                StrategyId = 2,
                Date = Convert.ToDateTime("01-01-2022"),
                Amount = 100,
                Strategy = new Strategy()
                {
                    StrategyId = 2,
                    Region = "EU"
                }
            };

            listPnl.Add(pnl1);

            //Act

            var results = _cumulativePnlService.CalculateCumulativePnlByRegion(listPnl);

            //Assert

            Assert.AreEqual(300, results.Select(s => s.Amount).First());
        }


        [Test]
        public void CreateCumulativePnlsListFail()
        {
            //Arrange
            var listPnl = new List<Pnl>();

            var pnl = new Pnl()
            {
                StrategyId = 2,
                Date = Convert.ToDateTime("01-01-2022"),
                Amount = 200,
                Strategy = new Strategy()
                {
                    StrategyId = 2,
                    Region = "EU"
                }
            };

            listPnl.Add(pnl);

            var pnl1 = new Pnl()
            {
                StrategyId = 2,
                Date = Convert.ToDateTime("01-01-2022"),
                Amount = 200,
                Strategy = new Strategy()
                {
                    StrategyId = 2,
                    Region = "EU"
                }
            };

            listPnl.Add(pnl1);

            //Act

            var results = _cumulativePnlService.CalculateCumulativePnlByRegion(listPnl);

            //Assert

            Assert.AreNotEqual(300, results.Select(s => s.Amount).First());
        }
    }
}
