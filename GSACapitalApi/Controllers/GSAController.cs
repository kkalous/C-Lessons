using GSASolution;
using GSASolution.Outputer;
using Microsoft.AspNetCore.Mvc;

namespace GSACapitalApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class GSAController : ControllerBase
    {
        private readonly GSAService _gsaService;

        public GSAController(GSAService csvOutputer)
        {
            _gsaService = csvOutputer;
        }

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
            var result = new List<string>();

            foreach (var line in _gsaService.GetCapitalsInfo(strategies))
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
            var result = new List<string>();

            foreach (var line in _gsaService.GetCumulativePnlsInfoWithStartingDate(region, startDate))
            {
                var rslt = @$"region: {region.ToUpper()}, date:{line.Date}, cumulativePnl:{line.Amount}";

                result.Add(rslt);
            };

            return result;
        }

        [HttpGet]
        [Route("/compound-daily-returns")]
        public List<string> GetCumulativePnl(string strategy)
        {
            var result = new List<string>();

            foreach (var line in _gsaService.GetCumpoundPnl(strategy))
            {
                var rslt = @$"strategy: {strategy}, date:{line.Date}, compoundReturn:{line.Amount}";

                result.Add(rslt);
            };

            return result;
        }



    }
}