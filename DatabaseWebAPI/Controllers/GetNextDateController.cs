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
    public class GetNextDateController : ApiController
    {
        RestClient restClient = new RestClient("http://localhost:50981/");
        public IHttpActionResult GetNextDate([FromUri] int id)
        {
            RestRequest centreRequest = new RestRequest("api/centres/", Method.Get);
            RestResponse centreResponse = restClient.Execute(centreRequest);
            List<Centre> centreList = JsonConvert.DeserializeObject<List<Centre>>(centreResponse.Content);

            RestRequest bookingsRequest = new RestRequest("api/bookings/", Method.Get);
            RestResponse bookingsResponse = restClient.Execute(bookingsRequest);
            List<Booking> bookingsList = JsonConvert.DeserializeObject<List<Booking>>(bookingsResponse.Content);

            if (bookingsList.Count > 0)
            {
                foreach (Centre item in centreList)
                {
                    if (item.Id.Equals(id))
                    {
                        foreach (Booking book in bookingsList)
                        {
                            if (book.CentreID.Equals(id))
                            {
                                DateTime latestDate = bookingsList.Max(r => r.FinishDate);
                                Booking newBooking = new Booking();
                                newBooking.StartDate = latestDate;
                                return Ok(latestDate.ToShortDateString());
                            }
                        }
                    }
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
