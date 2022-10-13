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
    public class GetBookingsController : ApiController
    {
        RestClient restClient = new RestClient("http://localhost:50981/");
        public IHttpActionResult GetBookings()
        {
            RestRequest bookingsRequest = new RestRequest("api/bookings/", Method.Get);
            RestResponse bookingsResponse = restClient.Execute(bookingsRequest);
            List<Booking> bookingsList = JsonConvert.DeserializeObject<List<Booking>>(bookingsResponse.Content);
            if (bookingsList.Count > 0)
            {
                Ok(bookingsList);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
