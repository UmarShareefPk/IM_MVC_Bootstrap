using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Helpers;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly APIHelpers apiHelpers;
        private readonly IConfiguration Configuration;
        public AccountController(APIHelpers apis , IConfiguration configuration)
        {
            apiHelpers = apis;
            Configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username , string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["ApiBaseUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));               
                var json = JsonConvert.SerializeObject(new { Username = username, Password = password });
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/Users/authenticate", stringContent);
                if (response.IsSuccessStatusCode)
                {                                  
                    var responseString = await response.Content.ReadAsStringAsync(); 
                    var userLogin = JsonConvert.DeserializeObject<UserLogin>(responseString);
                    HttpContext.Session.Set<UserLogin>("UserLogin", userLogin);                   

                    var Users = await apiHelpers.AllUsers();
                    HttpContext.Session.Set<List<User>>("Users", Users);

                    return Redirect("/Incident/IncidentListing");
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.BadRequest)
                        ViewBag.Error = "Incorrect username or password";
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        ViewBag.Error = "Server error. Please try later.";
                    ViewBag.Username = username;
                    return View();
                }
            }
        }
    }
}

