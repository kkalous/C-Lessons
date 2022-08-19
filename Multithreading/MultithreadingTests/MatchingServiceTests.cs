using Multhitreeading;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Multhitreading
{
    public class Tests
    {
        private MatchingService _matchingService;
        private string[] _names;
        [SetUp]
        public void Setup()
        {
            _matchingService = new MatchingService();
        }
  

        [Test]
        public void SameNamesAreNotCompared()
        {
            var list = new List<MatchModel>();
            _names = new string[]
            {
                    "Kamila",
                    "Kamila"
            };

            var result = _matchingService.GetNameMatches(_names);

            Assert.AreEqual(result.Count(), list.Count());
        }



        [Test]
        public void ReturnsMostSimilarNameForEachName()
        {
            _names = new string[]
            {
                    "Kamila",
                    "Kamilaa",
                    "Kamu",
            };

            var list = new List<MatchModel>()
            {
                new MatchModel
                {
                Name1 = "Kamila",
                Name2 = "Kamilaa"
                },
                new MatchModel
                {
                Name1 = "Kamilaa",
                Name2 = "Kamila"
                },
                new MatchModel
                {
                Name1 = "Kamu",
                Name2 = "Kamila"
                }
            };

            var results = _matchingService.GetNameMatches(_names);
            Assert.AreEqual(results.Select(s => new { s.Name1, s.Name2 }).ToString(), list.Select(s => new { s.Name1, s.Name2 }).ToString());

        }

        [Test]
        public void IfNamesNotSimilarAtAllReturnsNothing()
        {
            var list = new List<MatchModel>();
            _names = new string[]
            {
                    "Kamila",
                    "Zubejdo"
            };

            var result = _matchingService.GetNameMatches(_names);

            Assert.AreEqual(result.Count(), list.Count());
        }
    }
}