using DatabaseWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseWebAPI.Controllers
{
    public class LoginController : ApiController
    {
        Access access = new Access();
        public IHttpActionResult Login([FromUri] string username, [FromUri] string password)
        {
            string valUsername = access.username;
            string valPassword = access.password;
            if (username.Equals(valUsername) && password.Equals(valPassword))
            {
                return Json(new { Status = "Success"});
            }
            else
            {
                return Json(new { Status = "Failed" });
            }

        }
    }
}
