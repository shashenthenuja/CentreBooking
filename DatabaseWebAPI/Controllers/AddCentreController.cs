﻿using DatabaseWebAPI.Models;
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
    public class AddCentreController : ApiController
    {
        RestClient restClient = new RestClient("http://localhost:50981/");
        public IHttpActionResult AddCentre([FromBody] Centre centreData)
        {
            RestRequest restRequest1 = new RestRequest("api/centres/", Method.Get);
            RestResponse restResponse1 = restClient.Execute(restRequest1);
            List<Centre> data = JsonConvert.DeserializeObject<List<Centre>>(restResponse1.Content);

            int index = data.Count + 1;
            centreData.Id = index;
            RestRequest restRequest = new RestRequest("api/centres", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(centreData));
            RestResponse restResponse = restClient.Execute(restRequest);
            Centre result = JsonConvert.DeserializeObject<Centre>(restResponse.Content);
            if (result != null)
            {
                return Json(result);
            }
            return StatusCode(HttpStatusCode.NoContent);
            
        }
    }
}
