using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorseLeave.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public EmployeeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            ViewBag.Name = _httpContextAccessor.HttpContext.Session.GetString("Name");
            ViewBag.Role = _httpContextAccessor.HttpContext.Session.GetString("role");
        }
        public IActionResult Index()
        {
            ViewBag.Name = _httpContextAccessor.HttpContext.Session.GetString("Name");
            ViewBag.Role = _httpContextAccessor.HttpContext.Session.GetString("role");
            return View();
        }

        public IActionResult Profile()
        {
            ViewBag.Name = _httpContextAccessor.HttpContext.Session.GetString("Name");
            ViewBag.Role = _httpContextAccessor.HttpContext.Session.GetString("role");
            return View();
        }
        
        public IActionResult RequestList()
        {
            ViewBag.Name = _httpContextAccessor.HttpContext.Session.GetString("Name");
            ViewBag.Role = _httpContextAccessor.HttpContext.Session.GetString("role");
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult HistoryRequest()
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

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return Redirect("https://localhost:44304/Landing");
        }
    }
}
