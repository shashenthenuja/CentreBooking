using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebGUI.Models;

namespace WebGUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";

            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest request = new RestRequest("api/bookings/");
            RestResponse resp = restClient.Get(request);

            List<Booking> data = JsonConvert.DeserializeObject<List<Booking>>(resp.Content);

            return View(data);
        }

        [HttpPost]
        public IActionResult InsertCentre([FromBody] Centre centreData, string username, string password)
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest restRequest = new RestRequest("api/addcentre/?username=" + username + "&password=" + password, Method.Post);
            // check username and password are correctly added to the url ************************<<<<<<<<<<<<<<<<<<<<<<<
            restRequest.AddJsonBody(JsonConvert.SerializeObject(centreData));
            RestResponse restResponse = restClient.Execute(restRequest);
            Centre result = JsonConvert.DeserializeObject<Centre>(restResponse.Content);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult InsertBooking([FromBody] Booking bookingData)
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest restRequest = new RestRequest("api/addbooking/", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(bookingData));
            RestResponse restResponse = restClient.Execute(restRequest);
            Booking result = JsonConvert.DeserializeObject<Booking>(restResponse.Content);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult GetNextDate(int id)
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest restRequest = new RestRequest("api/getnextdate/{id}", Method.Post);
            restRequest.AddUrlSegment("id", id);
            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse.Content);
        }

        [HttpPost]
        public IActionResult ViewCentreBookings(int id)
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest restRequest = new RestRequest("api/addbooking/{id}", Method.Post);
            restRequest.AddUrlSegment("id", id);
            RestResponse restResponse = restClient.Execute(restRequest);
            List<Booking> result = JsonConvert.DeserializeObject<List<Booking>>(restResponse.Content);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
