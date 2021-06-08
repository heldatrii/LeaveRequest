using CorseLeave.Base;
using CorseLeave.Repository.Data;
using LeaveRequest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorseLeave.Controllers
{
    public class TypesController : BaseController<Tipe, TypeRepository, int>
    {
        private readonly TypeRepository repository;

        public TypesController(TypeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetUserData()
        {
            var result = await repository.Get();
            return Json(result);
        }


    }
}
