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

namespace Web.Controllers
{
    public class AccountController : Controller
    {
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
                client.BaseAddress = new Uri("https://localhost:44310/");
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
                    var u =  HttpContext.Session.Get<UserLogin>("UserLogin");
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
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44310/");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjE2ODE5RTc5LTI1QTMtNDZDOS04QjFGLThGQjZDNkY4QUM2MSIsIm5iZiI6MTYxNzczMzIxNCwiZXhwIjoxNjE4MzM4MDE0LCJpYXQiOjE2MTc3MzMyMTR9.fjKQcx-untL_uM-jn_3-2eVxCBszz6-K8xV2p8siV6Q");
            //    //GET Method  

            //    HttpResponseMessage response = await client.GetAsync("api/Incidents/GetIncidentsWithPage?PageSize=8&PageNumber=1&SortBy=q&SortDirection=q&Search=");
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string s = response.Content.ReadAsStringAsync().Result;
            //    }
            //    else
            //    {
            //    }
            //}           
         
        }
    }
}

