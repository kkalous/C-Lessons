using GSASolution;
using GSASolution.Outputer;
using Microsoft.AspNetCore.Mvc;

namespace GSACapitalApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class GSAController : ControllerBase
    {
        [HttpGet]
        [Route("/test")]
        public string GetTest()
        {
            return "Hello!";
        }

        [HttpGet]
        [Route("/monthly-capital")]
        public List<string> GetMonthlyCapital(string strategies)
        {
            CsvOutputer _outputer = new CsvOutputer(new PnlService());

            var result = new List<string>();

            foreach (var line in _outputer.GetCapitalsInfo(strategies))
            {
                var rslt = @$"{line.Strategy.StrategyName}, date:{line.Date}, capital:{line.Amount}";
                result.Add(rslt);              
            };

            return result;

        }


        [HttpGet]
        [Route("/cumulative-pnl")]
        public List<string> GetCumulativePnl(string region, DateTime? startDate = null)
        {
            CsvOutputer _outputer = new CsvOutputer(new PnlService());

            var result = new List<string>();

            foreach (var line in _outputer.GetCumulativePnlsInfoWithStartingDate(region, startDate))
            {
                var rslt = @$"region: {region.ToUpper()}, date:{line.Date}, cumulativePnl:{line.Amount}";

                result.Add(rslt);
            };

            return result;
        }



    }
}