using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorseLeave.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public ManagerController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            //if (_httpContextAccessor.HttpContext.Session.GetString("role") == null)
            //{
            //    Logout();
            //}
            //else if(_httpContextAccessor.HttpContext.Session.GetString("role") == "Employee")
            //{
            //    Redirect("https://localhost:44304/Employee");
            //}
            ViewBag.Name = _httpContextAccessor.HttpContext.Session.GetString("Name");
            ViewBag.Role = _httpContextAccessor.HttpContext.Session.GetString("role");
        }

        public IActionResult Index()
        {
            ViewBag.NIK = _httpContextAccessor.HttpContext.Session.GetString("NIK");
            ViewBag.Judul = "Dasboard";
            ViewBag.Role = _httpContextAccessor.HttpContext.Session.GetString("role");
            ViewBag.Name = _httpContextAccessor.HttpContext.Session.GetString("Name");
            return View();
        }

        public IActionResult Profile()
        {
            ViewBag.Name = _httpContextAccessor.HttpContext.Session.GetString("Name");
            ViewBag.Role = _httpContextAccessor.HttpContext.Session.GetString("role");
            return View();
        }

        public IActionResult LeaveRequestList()
        {
            ViewBag.Name = _httpContextAccessor.HttpContext.Session.GetString("Name");
            ViewBag.Role = _httpContextAccessor.HttpContext.Session.GetString("role");
            return View();
        }

        [HttpGet]
        public string GetNIK()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("NIK");
        }

        public string GetRole()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("role");
        }

        public ActionResult ToLanding()
        {
            return Redirect("https://localhost:44304/Landing");
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return Redirect("https://localhost:44304/Landing");
        }
    }
}
