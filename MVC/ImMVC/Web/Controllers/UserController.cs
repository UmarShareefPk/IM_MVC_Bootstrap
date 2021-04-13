using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Helpers;
using Microsoft.Extensions.Configuration;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly APIHelpers apiHelpers;
        private readonly IConfiguration Configuration;
        public UserController(APIHelpers apis, IConfiguration configuration)
        {
            apiHelpers = apis;
            Configuration = configuration;
        }


        [HttpGet]
        public IActionResult AddUser()
        {
            var user = HttpContext.Session.Get<UserLogin>("UserLogin");
            ViewBag.userLogin = JsonConvert.SerializeObject(user);
            ViewBag.token = user.Token;
            ViewBag.userId = user.user.Id;
            ViewBag.baseUrl = Configuration["ApiBaseUrl"];

            var allUsers = HttpContext.Session.Get<List<User>>("Users");
            ViewBag.Users = JsonConvert.SerializeObject(allUsers);

            return View();
        }

        public IActionResult UserListing()
        {
            var user = HttpContext.Session.Get<UserLogin>("UserLogin");
            ViewBag.userLogin = JsonConvert.SerializeObject(user);
            ViewBag.token = user.Token;
            ViewBag.baseUrl = Configuration["ApiBaseUrl"];

            var allUsers = HttpContext.Session.Get<List<User>>("Users");
            ViewBag.Users = JsonConvert.SerializeObject(allUsers);

            return View();
        }


    }// end of class
}
