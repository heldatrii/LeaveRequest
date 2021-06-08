using CorseLeave.Base;
using CorseLeave.Repository.Data;
using LeaveRequest.Models;
using LeaveRequest.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorseLeave.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;

        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> ApplyListManager(string NIK)
        {
            var result = await repository.ApplyListManager(NIK);
            return Json(result);
        }
        
        public async Task<JsonResult> ApplyListID(string NIK)
        {
            var result = await repository.ApplyListID(NIK);
            return Json(result);
        }

        public async Task<JsonResult> ApplyDetail(string IdRequest)
        {
            var result = await repository.ApplyDetail(IdRequest);
            return Json(result);
        }

        public JsonResult CheckPassword(CheckPasswordVM checkPasswordVM)
        {
            var result = repository.CheckPassword(checkPasswordVM);
            return Json(result);
        }
    }
}
