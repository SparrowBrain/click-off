﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.SignalR;

namespace ClickOff.REST.Controllers
{
    public class ResultsController: ApiController
    {
        [HttpPost]
        [Route("api/Results")]
        public HttpResponseMessage PublishResults(string name)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ResultHub>();
            context.Clients.All.broadcastMessage(name, "Asas");
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}