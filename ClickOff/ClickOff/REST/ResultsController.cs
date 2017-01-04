
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClickOff.Hubs;
using Microsoft.AspNet.SignalR;

namespace ClickOff.REST
{
    public class ResultsController: ApiController
    {
        private static readonly Dictionary<string, double> Multipliers = new Dictionary<string, double>
        {
            {"million", Math.Pow(10, 6)},
            {"billion", Math.Pow(10, 9)},
            {"trillion", Math.Pow(10, 12)},
            {"quadrillion", Math.Pow(10, 15)},
            {"quintillion", Math.Pow(10, 18)},
            {"sextillion", Math.Pow(10, 21)},
            {"septillion", Math.Pow(10, 24)},
            {"octillion", Math.Pow(10, 27)},
            {"nonillion", Math.Pow(10, 30)},
            {"decillion", Math.Pow(10, 33)},
            {"undecillion", Math.Pow(10, 36)},
            {"duodecillion", Math.Pow(10, 39)},
            {"tredecillion", Math.Pow(10, 42)},
            {"quattuordecillion", Math.Pow(10, 45)},
            {"quindecillion", Math.Pow(10, 48)}
        };

        [HttpPost]
        [Route("api/Results")]
        public HttpResponseMessage PublishResults(string name, Request request)
        {
            var resultRequest = new ResultRequest
            {
                CookiesBaked = ParseShortNumber(request.CookiesBaked),
                CookiesInBank = ParseShortNumber(request.CookiesInBank),
                Cps = ParseShortNumber(request.Cps)
            };

            var context = GlobalHost.ConnectionManager.GetHubContext<ResultHub>();
            context.Clients.All.updateCookies(name, resultRequest);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private double ParseShortNumber(string number)
        {
            var numberParts = number.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if (numberParts.Length == 0)
            {
                return double.NaN;
            }

            double digitPart;
            if (!double.TryParse(numberParts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out digitPart))
            {
                return double.NaN;
            }
            
            if (numberParts.Length < 2)
            {
                return digitPart;
            }

            double multiplier;
            if (!Multipliers.TryGetValue(numberParts[1], out multiplier))
            {
                return double.NaN;
            }

            return digitPart * multiplier;
        }
    }
}