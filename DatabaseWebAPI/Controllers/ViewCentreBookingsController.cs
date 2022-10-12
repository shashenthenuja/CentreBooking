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
    public class ViewCentreBookingsController : ApiController
    {
        RestClient restClient = new RestClient("http://localhost:50981/");
        public IHttpActionResult ViewCentreBooking([FromUri] int id)
        {
            RestRequest centreRequest = new RestRequest("api/centres/", Method.Get);
            RestResponse centreResponse = restClient.Execute(centreRequest);
            List<Centre> centreList = JsonConvert.DeserializeObject<List<Centre>>(centreResponse.Content);

            RestRequest bookingsRequest = new RestRequest("api/bookings/", Method.Get);
            RestResponse bookingsResponse = restClient.Execute(bookingsRequest);
            List<Booking> bookingsList = JsonConvert.DeserializeObject<List<Booking>>(bookingsResponse.Content);

            List<Booking> result = new List<Booking>();

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
                                result.Add(book);
                            }
                        }
                    }
                }
            }
            if (result.Count > 0)
            {
                return Json(result);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
