
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClickOff.Hubs;
using Microsoft.AspNet.SignalR;

namespace ClickOff.REST
{
    public class ResultsController: ApiController
    {
        [HttpPost]
        [Route("api/Results")]
        public HttpResponseMessage PublishResults(string name, ResultRequest request)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ResultHub>();
            context.Clients.All.updateCookies(name, request);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}