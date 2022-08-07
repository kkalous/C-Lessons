﻿using Data.Scaffolded;
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
        private CumulativePnlService _cumulativePnlService;

        [SetUp]
        public void Setup()
        {
            _cumulativePnlService = new CumulativePnlService();
        }

        [Test]
        public void CreateCumulativePnlsList()
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

            var results = _cumulativePnlService.CalculateCumulativePnl(listPnl);

            //Assert

            Assert.AreEqual(300, results.Select(s => s.Amount).First());
        }
    }
}