using LeaveRequest.Base;
using LeaveRequest.Context;
using LeaveRequest.Models;
using LeaveRequest.Repository.Data;
using LeaveRequest.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LeaveRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestStatusController : BaseController<RequestStatus, RequestStatusRepository, string>
    {
        private readonly RequestStatusRepository requestStatusRepository;
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public RequestStatusController(RequestStatusRepository requestStatusRepository, MyContext myContext, IConfiguration configuration) : base(requestStatusRepository)
        {
            this.requestStatusRepository = requestStatusRepository;
            this.myContext = myContext;
            _configuration = configuration;
        }

        [HttpGet("GetByRequestId/{requestId}")]
        public IActionResult GetByRequestId(int requestId)
        {
            RequestStatus requestStatus = myContext.RequestStatuses.FirstOrDefault(rs => rs.RequestId == requestId);
            return Ok(requestStatus);
        }

        [HttpPost("LeaveRequestResponse")]
        public IActionResult LeaveRequestResponse(ResponseVM responseVM)
        {
            var person = myContext.Persons.FirstOrDefault(p => p.NIK == responseVM.NIK);
            
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("hidgastg@gmail.com", "inayah71"),
                EnableSsl = true
            };
            client.Send("hidgastg@gmail.com", person.Email, "LEAVE REQUEST RESPONSE", $"Hello {person.FirstName} {person.LastName} \nYour Leave Request has been {responseVM.Response}");
            return Ok("Request Sent");

        }
    }
}
