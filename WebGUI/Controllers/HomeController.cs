using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public IActionResult InsertCentre([FromBody] Centre centreData)
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest restRequest = new RestRequest("api/addcentre/", Method.Post);
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

        public IActionResult GetNextDate(int id)
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest restRequest = new RestRequest("api/getnextdate/{id}", Method.Get);
            restRequest.AddUrlSegment("id", id);
            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse.Content);
        }

        [HttpPost]
        public IActionResult ViewCentreBookings(int id)
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest restRequest = new RestRequest("api/viewcentrebookings/{id}", Method.Post);
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

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest restRequest = new RestRequest("api/login/?username=" + username + "&password=" + password, Method.Post);
            RestResponse restResponse = restClient.Execute(restRequest);
            var data = (JObject)JsonConvert.DeserializeObject(restResponse.Content, new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });
            var status = (string)data["Status"];
            if (status.Equals("Success"))
            {
                return Ok();
            }
            return BadRequest();
        }

        public IActionResult GetCentres()
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest request = new RestRequest("api/getcentres/", Method.Get);
            RestResponse restResponse = restClient.Execute(request);
            List<Centre> data = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult GetBookings(int id)
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest request = new RestRequest("api/bookings/");
            RestResponse resp = restClient.Get(request);
            List<Booking> data = JsonConvert.DeserializeObject<List<Booking>>(resp.Content);

            List<Booking> result = new List<Booking>();
            foreach (Booking item in data)
            {
                if (item.CentreId.Equals(id))
                {
                    result.Add(item);
                }
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult SearchCentre(string name)
        {
            RestClient restClient = new RestClient("http://localhost:50981/");
            RestRequest request = new RestRequest("api/getcentres/", Method.Get);
            RestResponse restResponse = restClient.Execute(request);
            List<Centre> data = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);

            Centre centre = new Centre();

            foreach (Centre item in data)
            {
                if (item.CentreName.ToLower().Contains(name))
                {
                    centre = item;
                    break;
                }
            }

            if (centre != null)
            {
                return Ok(centre.CentreName);
            }
            return BadRequest();
        }
    }
}
