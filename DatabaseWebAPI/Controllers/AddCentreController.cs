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
    public class AddCentreController : ApiController
    {
        RestClient restClient = new RestClient("http://localhost:50981/");
        Access access = new Access();
        public IHttpActionResult AddCentre([FromBody] Centre centreData, [FromUri] string username, [FromUri] string password)
        {
            string valUsername = access.username;
            string valPassword = access.password;
            if (username.Equals(valUsername) && password.Equals(valPassword))
            {
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
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            
        }
    }
}
