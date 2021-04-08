using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Helpers
{
    public class APIHelpers
    {
        IHttpContextAccessor accessor;
        public APIHelpers(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }
        public  async Task<Incident> GetIncidentById(string Id)
        {
            using (var client = new HttpClient())
            {              
                var user = accessor.HttpContext.Session.Get<UserLogin>("UserLogin");
                client.BaseAddress = new Uri("https://localhost:44310/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

                HttpResponseMessage response = await client.GetAsync("api/Incidents/IncidentById?Id=" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var incident = JsonConvert.DeserializeObject<Incident>(responseString);
                    return incident;
                }
                else
                {
                    return null;
                }
            }
        } //

        public async Task<List<User>> AllUsers()
        {
            using (var client = new HttpClient())
            {
                var user = accessor.HttpContext.Session.Get<UserLogin>("UserLogin");
                client.BaseAddress = new Uri("https://localhost:44310/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

                HttpResponseMessage response = await client.GetAsync("api/users/AllUsers");
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<User>>(responseString);
                    return users;
                }
                else
                {
                    return null;
                }
            }
        } //


    }// end of class
}
