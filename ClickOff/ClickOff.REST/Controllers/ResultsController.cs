using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ClickOff.REST.Controllers
{
    public class ResultsController: ApiController
    {
        [HttpPost]
        [Route("api/Results")]
        public void PublishResults(string name)
        {
            
        }
    }
}