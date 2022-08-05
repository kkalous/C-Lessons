using GSASolution;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanParseTextFile()
        {
            var applicationDirectory = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            var fileLoc = applicationDirectory + @"\Resources\properties.csv";

            //            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData", "Output"));

            var reader = new CsvInputer();
            var result = reader.ReadLines(fileLoc).ToList();
            Assert.That(result.Count, Is.EqualTo(16));
        }


        [Test]
        public void CanParseValidStrategies()
        {
            var lines = new[] { "StratName,Region", "Strategy1,AP", "Strategy2,EU" };

            var reader = new CsvInputer();
            var result = reader.ParseStrategies(lines).ToList();
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First().StrategyName, Is.EqualTo("Strategy1"));
            Assert.That(result[1].Region, Is.EqualTo("EU"));
        }
    }
}