using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Helpers;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class IncidentController : Controller
    {
        public IActionResult IncidentListing()
        {
            var user = HttpContext.Session.Get<UserLogin>("UserLogin");
            ViewBag.userLogin = JsonConvert.SerializeObject(user);
            ViewBag.token = user.Token;
            return View();
        }
    }
}
