using LeaveRequest.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CorseLeave.Controllers
{
    public class LandingController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public LandingController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        public string GetRole()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("role");
        }

        [HttpPost]
        public string Login(LoginVM loginVM)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44313/API/Accounts/Login", content).Result;


            var token = result.Content.ReadAsStringAsync().Result;
            HttpContext.Session.SetString("token", token);

            if (result.IsSuccessStatusCode)
            {
                var jwtReader = new JwtSecurityTokenHandler();
                var jwt = jwtReader.ReadJwtToken(token);
                var NIK = jwt.Claims.First(c => c.Type == "NIK").Value;
                var Name = jwt.Claims.First(c => c.Type == "Name").Value;
                //var name = jwt.Claims.First(c => c.Type == "FirstName").Value;
                HttpContext.Session.SetString("NIK", NIK);
                HttpContext.Session.SetString("Name", Name);
                ViewData["NIK"] = HttpContext.Session.GetString("NIK");
                //HttpContext.Session.SetString("firstName", name);
                var role = jwt.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
                HttpContext.Session.SetString("role", role);
                ViewData["role"] = HttpContext.Session.GetString("role");
                ViewData["Name"] = HttpContext.Session.GetString("Name");
                if (role == "Manager")
                {
                    return Url.Action("", "Manager");
                }
                else if (role == "Employee")
                {
                    return Url.Action("", "Employee");
                }
                else
                {
                    return Url.Action("", "Landing");
                }
            }
            else
            {
                return Url.Action("Error", "Home");
            }
        }
    }
}
