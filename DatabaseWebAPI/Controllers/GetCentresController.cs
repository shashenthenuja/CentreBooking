using DatabaseWebAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseWebAPI.Controllers
{
    public class GetCentresController : ApiController
    {
        RestClient restClient = new RestClient("http://localhost:50981/");
        public IHttpActionResult GetCentres()
        {
            RestRequest restRequest = new RestRequest("api/centres/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);
            List<Centre> result = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);
            if (result != null)
            {
                return Json(result);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
