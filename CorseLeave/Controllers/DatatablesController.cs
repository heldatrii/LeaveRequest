using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorseLeave.Controllers
{
    public class DatatablesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
