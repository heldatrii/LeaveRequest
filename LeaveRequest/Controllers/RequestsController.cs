using LeaveRequest.Base;
using LeaveRequest.Context;
using LeaveRequest.Models;
using LeaveRequest.Repository.Data;
using LeaveRequest.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class RequestsController : BaseController<Request, RequestRepository, int>
    {
        private readonly RequestRepository requestRepository;
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public RequestsController(RequestRepository requestRepository, MyContext myContext, IConfiguration configuration) : base(requestRepository)
        {
            this.requestRepository = requestRepository;
            this.myContext = myContext;
            _configuration = configuration;
        }

        [HttpGet("GetByRequestId/{requestId}")]
        public IActionResult GetByRequestId(int requestId)
        {
            Request requestStatus = myContext.Requests.FirstOrDefault(rs => rs.RequestId == requestId);
            return Ok(requestStatus);
        }

        [HttpGet("RequestType")]
        public async Task<IActionResult> RequestType()
        {
            var typeKind = await (
                from request in myContext.Requests
                join type in myContext.Tipes
                on request.TipeId equals type.TipeId
                select new
                {
                    NameTipe = type.NameTipe,
                    TypeKind = type.TypeKind
                }
                ).ToListAsync();
            return Ok(typeKind);
        }

        [HttpPost("LeaveRequestResponse")]
        public IActionResult LeaveRequestResponse(ResponseVM responseVM)
        {
            var person = myContext.Persons.FirstOrDefault(p => p.NIK == responseVM.NIK);
            var manager = myContext.Persons.FirstOrDefault(p => p.NIK == person.ManagerId);
            var request = myContext.Requests.FirstOrDefault(r => r.RequestId == responseVM.IdRequest);
            var type = myContext.Tipes.FirstOrDefault(t => t.TipeId == request.TipeId);

            string statusResponse = responseVM.Response;
            string status = "";
            string statusColor = "";

            if (responseVM.Response == "Approved")
            {
                status = "Diterima";
                statusColor = "green";
            }
            if (responseVM.Response == "Rejected")
            {
                status = "Ditolak";
                statusColor = "red";
            }

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("hidgastg@gmail.com");
            mail.To.Add(person.Email);
            mail.Subject = $"LEAVE REQUEST RESPONSE (ID: {responseVM.IdRequest})";

            mail.IsBodyHtml = true;
            string htmlBody;

            //htmlBody = $"<h1>Hello {person.FirstName} {person.LastName}</h1>";
            htmlBody = "<body>";
            htmlBody += "<div style='margin-left: 300px; margin-right: 300px;'>";
            htmlBody += "<div style='border: 2px solid black; background-color: #7c97ece3; border-radius: 25px 25px 0 0;'>";
            htmlBody += "<h1 style='margin-top: 10px; margin-bottom: 10px; text-align: center; color: white; '>LEAVE REQUEST APPROVEMENT</h1>";
            htmlBody += "</div>";
            htmlBody += "<div style='border-left: 2px solid black; border-right: 2px solid black;'>";
            htmlBody += "<div style='padding: 25px;'>";
            htmlBody += $"<p>Dear <b>{person.FirstName} {person.LastName}</b></p>";
            htmlBody += "<p></p>";
            htmlBody += "<p>Melalui email ini kami informasikan bahwa pengajuan cuti dengan : </p>";
            htmlBody += "<table>";
            htmlBody += "<tr>";
            htmlBody += $"<td>ID Request</td><td>:</td><td>{responseVM.IdRequest}</td>";
            htmlBody += "</tr>";
            htmlBody += "<tr>";
            htmlBody += $"<td>Tanggal Dimulai</td><td>:</td><td>{request.StartDate.ToString("MMMM dd, yyyy")}</td>";
            htmlBody += "</tr>";
            htmlBody += "<tr>";
            htmlBody += $"<td>Tanggal Berakhir</td><td>:</td><td>{request.EndDate.ToString("MMMM dd, yyyy")}</td>";
            htmlBody += "</tr>";
            htmlBody += "<tr>";
            htmlBody += $"<td>Alasan</td><td>:</td><td>{type.NameTipe}</td>";
            htmlBody += "</tr>";
            htmlBody += "</table>";
            htmlBody += $"<p>Telah <b style='color: {statusColor}'>{status}</b></p>";
            htmlBody += "<p></p>";
            htmlBody += "<p>Terimakasih dan selalu jaga kesehatan</p>";
            htmlBody += "<p style='text-align: right;'>Best Regards</p>";
            htmlBody += $"<p style='text-align: right;'><b>{manager.FirstName} {manager.LastName}</b></p>";
            htmlBody += "</div>";
            htmlBody += "</div>";
            htmlBody += "<div style='border: 2px solid black; background-color: #7c97ece3; border-radius: 0 0 25px 25px;'>";
            htmlBody += "<p style='text-align: center; color: white;'>APL Tower Lantai 37 Jl. Letjen S. Parman Kav. 28 Jakarta 11470</p>";
            htmlBody += "<p style='text-align: center; color: white;'>contact@mii.co.id</p>";
            htmlBody += "<p style='text-align: center; color: white;'>+62 21 29345 777</p>";
            htmlBody += "</div>";
            htmlBody += "</div>";
           

            mail.Body = htmlBody;
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("hidgastg@gmail.com", "inayah71"),
                EnableSsl = true
            };

            client.Send(mail);
            return Ok("Request Sent");

        }
    }
}
