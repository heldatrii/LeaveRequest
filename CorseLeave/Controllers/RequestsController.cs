﻿using CorseLeave.Base;
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
    public class RequestsController : BaseController<Request, RequestRepository, int>
    {
        private readonly RequestRepository repository;

        public RequestsController(RequestRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LeaveRequestResponse(ResponseVM responseVM)
        {
            var result = repository.LeaveRequestResponse(responseVM);
            return Json(result);
        }

        public async Task<JsonResult> GetByRequestId(int RequestId)
        {
            var result = await repository.GetByRequestId(RequestId);
            return Json(result);
        }


    }
}
