using DatabaseWebAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseWebAPI.Controllers
{
    public class AddBookingController : ApiController
    {
        RestClient restClient = new RestClient("http://localhost:50981/");
        public IHttpActionResult AddBooking([FromBody] Booking bookingData)
        {
            RestRequest centreRequest = new RestRequest("api/centres/", Method.Get);
            RestResponse centreResponse = restClient.Execute(centreRequest);
            List<Centre> centreList = JsonConvert.DeserializeObject<List<Centre>>(centreResponse.Content);

            RestRequest bookingsRequest = new RestRequest("api/bookings/", Method.Get);
            RestResponse bookingsResponse = restClient.Execute(bookingsRequest);
            List<Booking> bookingsList = JsonConvert.DeserializeObject<List<Booking>>(bookingsResponse.Content);

            int count = bookingsList.Where(s => s.CentreID.Equals(bookingData.CentreID)).Count();

            if (count > 0)
            {
                foreach (Booking item in bookingsList)
                {
                    if (item.CentreID.Equals(bookingData.CentreID))
                    {
                        DateTime latestDate = bookingsList.Where(s => s.CentreID.Equals(bookingData.CentreID)).Max(r => r.FinishDate);
                        if (bookingData.StartDate >= latestDate)
                        {
                            int index = bookingsList.Count + 1;
                            bookingData.Id = index;
                            RestRequest restRequest = new RestRequest("api/bookings/", Method.Post);
                            restRequest.AddJsonBody(JsonConvert.SerializeObject(bookingData));
                            RestResponse restResponse = restClient.Execute(restRequest);
                            Booking result = JsonConvert.DeserializeObject<Booking>(restResponse.Content);
                            if (result != null)
                            {
                                return Json(result);
                            }
                            return StatusCode(HttpStatusCode.NoContent);
                        }
                    }
                }
            }
            else
            {
                int index = bookingsList.Count + 1;
                bookingData.Id = index;
                RestRequest restRequest = new RestRequest("api/bookings/", Method.Post);
                restRequest.AddJsonBody(JsonConvert.SerializeObject(bookingData));
                RestResponse restResponse = restClient.Execute(restRequest);
                Booking result = JsonConvert.DeserializeObject<Booking>(restResponse.Content);
                if (result != null)
                {
                    return Json(result);
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
