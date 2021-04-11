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
using Microsoft.Extensions.Configuration;

namespace Web.Controllers
{
    [SessionCheck]
    public class IncidentController : Controller
    {

        private readonly APIHelpers apiHelpers;
        private readonly IConfiguration Configuration;      
        public IncidentController(APIHelpers apis , IConfiguration configuration)
        {
            apiHelpers = apis;
            Configuration = configuration;
        }
        public IActionResult IncidentListing()
        {
            var user = HttpContext.Session.Get<UserLogin>("UserLogin");
            ViewBag.userLogin = JsonConvert.SerializeObject(user);
            ViewBag.token = user.Token;
            ViewBag.baseUrl = Configuration["ApiBaseUrl"];

            var allUsers = HttpContext.Session.Get<List<User>>("Users");
            ViewBag.Users = JsonConvert.SerializeObject(allUsers);

            return View();
        }

        public async Task<IActionResult> IncidentDetails(string Id)
        {
            
            if (string.IsNullOrEmpty(Id))
                return Redirect("~/Home/CustomError");

            var user = HttpContext.Session.Get<UserLogin>("UserLogin");
            ViewBag.userLogin = JsonConvert.SerializeObject(user);
            ViewBag.token = user.Token;
            ViewBag.userId = user.user.Id;
            ViewBag.baseUrl = Configuration["ApiBaseUrl"];

            var incident = await apiHelpers.GetIncidentById(Id);
            if (incident == null)
                return Redirect("~/Home/CustomError");
            
            return View(incident);            
        }

        [HttpGet]
        public IActionResult AddIncident()
        {
            var user = HttpContext.Session.Get<UserLogin>("UserLogin");
            ViewBag.userLogin = JsonConvert.SerializeObject(user);
            ViewBag.token = user.Token;
            ViewBag.baseUrl = Configuration["ApiBaseUrl"];

            var allUsers = HttpContext.Session.Get<List<User>>("Users");
            ViewBag.Users = JsonConvert.SerializeObject(allUsers);

            return View();
        }

    }
}
