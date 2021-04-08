using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Web.Controllers
{
    [SessionCheck]
    public class IncidentController : Controller
    {

        private readonly APIHelpers apiHelpers;
        public IncidentController(APIHelpers apis)
        {
            apiHelpers = apis;
        }
        public IActionResult IncidentListing()
        {
            var user = HttpContext.Session.Get<UserLogin>("UserLogin");
            ViewBag.userLogin = JsonConvert.SerializeObject(user);
            ViewBag.token = user.Token;

            var allUsers = HttpContext.Session.Get<List<User>>("Users");
            ViewBag.Users = JsonConvert.SerializeObject(allUsers);

            return View();
        }

        public async Task<IActionResult> IncidentDetails(string Id)
        {
            //var apis = new APIHelpers();
            if (string.IsNullOrEmpty(Id))
                return Redirect("~/Home/CustomError");

            var user = HttpContext.Session.Get<UserLogin>("UserLogin");
            ViewBag.userLogin = JsonConvert.SerializeObject(user);
            ViewBag.token = user.Token;           
           

            var incident = await apiHelpers.GetIncidentById(Id);
            if (incident == null)
                return Redirect("~/Home/CustomError");
            
            return View(incident);            
        }

    }
}
