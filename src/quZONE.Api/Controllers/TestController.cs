using System.Web.Http;
using quZONE.Common.Logging;
using quZONE.Domain;
//using System.Web.Mvc;


namespace quZONE.Api.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : BaseApiController
    {
        private readonly ITestService _testService;

        ////public TestController()
        ////{

        ////}

        readonly Logger _logger = new Logger();

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [Route("rate2")]
        public string Get()
        {
            return _testService.GetQuote();
            //return "111"; // To be re-implemented

        }

        [Route("log")]
        public string Testlog()
        {
            _logger.Info("Then event logged!");
            _logger.Error("The error occures...");

            return "logged...";
        }

        //[Route("error")]
        //public string Error()
        //{

        //    var bottom = 0;

        //    var result = 1/bottom;

        //    return "Error handled...";
        //}

        
    }
}
