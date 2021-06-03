using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using LeaveRequest.Base;
using LeaveRequest.Context;
using LeaveRequest.Models;
using LeaveRequest.Repository.Data;
using LeaveRequest.Encryptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Security.Claims;
using LeaveRequest.ViewModel;
using System.IdentityModel.Tokens.Jwt;

namespace LeaveRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Consumes("application/json")]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, MyContext myContext, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.myContext = myContext;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterVM registerVM)
        {
            try
            {
                Person person = new Person();
                person.NIK = registerVM.NIK;
                person.IdDepartement = registerVM.IdDepartement;
                person.ManagerId = registerVM.ManagerId;
                person.UserName = registerVM.UserName;
                person.FirstName = registerVM.FirstName;
                person.LastName = registerVM.LastName;
                person.Email = registerVM.Email;
                person.BirthDate = registerVM.BirthDate;
                person.Phone = registerVM.Phone;
                myContext.Persons.Add(person);
                myContext.SaveChanges();

                try
                {
                    Account account = new Account();
                    account.NIK = registerVM.NIK;
                    account.Password = Hashing.HashPassword(registerVM.Password);
                    myContext.Accounts.Add(account);
                    myContext.SaveChanges();

                    try
                    {
                        var roleId = myContext.Roles.Where(r => r.NameRole == "Employee").FirstOrDefault().Id;
                        RoleAccount roleAccount = new RoleAccount();
                        roleAccount.NIK = registerVM.NIK;
                        roleAccount.IdRole = roleId;
                        myContext.RoleAccounts.Add(roleAccount);
                        myContext.SaveChanges();

                        try
                        {
                            LeaveAllowance leaveAllowance = new LeaveAllowance();
                            leaveAllowance.NIK = registerVM.NIK;
                            leaveAllowance.LeaveAllow = 12;
                            leaveAllowance.UsedLeaveAllow = 0;
                            myContext.LeaveAllowances.Add(leaveAllowance);
                            myContext.SaveChanges();
                            return Ok();
                        }
                        catch (Exception)
                        {
                            var delPerson = myContext.Persons.Find(registerVM.NIK);
                            myContext.Persons.Remove(delPerson);
                            var resultPerson = myContext.SaveChanges();

                            var delAccount = myContext.Accounts.Find(registerVM.NIK);
                            myContext.Accounts.Remove(delAccount);
                            var resultAccount = myContext.SaveChanges();

                            var delRoleAccount = myContext.RoleAccounts.Find(registerVM.NIK);
                            myContext.RoleAccounts.Remove(delRoleAccount);
                            var resultRoleAccount = myContext.SaveChanges();

                            return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Can't Insert New Data" });
                        }
                    }
                    catch (Exception)
                    {
                        var delPerson = myContext.Persons.Find(registerVM.NIK);
                        myContext.Persons.Remove(delPerson);
                        var resultPerson = myContext.SaveChanges();

                        var delAccount = myContext.Accounts.Find(registerVM.NIK);
                        myContext.Accounts.Remove(delAccount);
                        var resultAccount = myContext.SaveChanges();

                        return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Role Not Found" });
                    }
                }
                catch (Exception)
                {
                    var delPerson = myContext.Persons.Find(registerVM.NIK);
                    myContext.Persons.Remove(delPerson);
                    var result = myContext.SaveChanges();
                    return StatusCode(405, new { status = HttpStatusCode.MethodNotAllowed, message = "Password Data Type Not Allowed" });
                }
            }
            catch (Exception)
            {
                return StatusCode(400, new { status = HttpStatusCode.MethodNotAllowed, message = "Bad Request" });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginVM loginVM)
        {
            var person = myContext.Persons.FirstOrDefault(p => p.Email == loginVM.Email);
            if (person != null)
            {
                var account = myContext.Accounts.FirstOrDefault(p => p.NIK == person.NIK);
                if (account != null && Hashing.VerifyPassword(loginVM.Password, account.Password))
                {
                    var roleAccount = myContext.RoleAccounts.Where(ra => ra.NIK == account.NIK).ToList();
                    var claims = new List<Claim> {
                    new Claim("NIK", person.NIK),
                    new Claim("FirstName", person.FirstName),
                    new Claim("LastName", person.LastName),
                    new Claim("Email", person.Email)
                    };
                    foreach (var item in roleAccount)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, myContext.Roles.Where(r => r.Id == item.IdRole).FirstOrDefault().NameRole));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddMinutes(10), signingCredentials: signIn);

                    return StatusCode(200, new { status = HttpStatusCode.OK, message = "", person.Email, token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                else
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Wrong Password" });
                }
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Email Not Found" });
            }
        }

        //[Authorize(Roles = "Manager")]
        [HttpGet("ApplyList")]
        public async Task<IActionResult> ApplyList()
        {
            var data = await (
                from departement in myContext.Departements
                join person in myContext.Persons
                on departement.Id equals person.IdDepartement
                join requestStatus in myContext.RequestStatuses
                on person.NIK equals requestStatus.NIK
                join request in myContext.Requests
                on requestStatus.IdRequest equals request.Id
                join requestType in myContext.RequestTypes
                on request.Id equals requestType.IdRequest
                join tipe in myContext.Tipes 
                on requestType.IdRequest equals tipe.Id
                select new
                {
                    NIK = person.NIK,
                    IdDepartement = person.IdDepartement,
                    DepartementName = departement.Name,
                    ManagerId = person.ManagerId,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    BirthDate = person.BirthDate,
                    Phone = person.Phone,
                    IdRequest = requestStatus.IdRequest,
                    Status = requestStatus.Status,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    IdType = requestType.IdType,
                    Type = tipe.NameTipe

                }
                ).ToListAsync();
            return Ok(data);
        }
        
        [HttpGet("ApplyBy/{NIK}")]
        public IActionResult ApplyBy(string NIK)
        {
            var data = (
                from departement in myContext.Departements
                join person in myContext.Persons
                on departement.Id equals person.IdDepartement
                join requestStatus in myContext.RequestStatuses
                on person.NIK equals requestStatus.NIK
                join request in myContext.Requests
                on requestStatus.IdRequest equals request.Id
                join requestType in myContext.RequestTypes
                on request.Id equals requestType.IdRequest
                where person.NIK == NIK
                select new
                {
                    NIK = person.NIK,
                    IdDepartement = person.IdDepartement,
                    DepartementName = departement.Name,
                    ManagerId = person.ManagerId,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    BirthDate = person.BirthDate,
                    Phone = person.Phone,
                    IdRequest = requestStatus.IdRequest,
                    Status = requestStatus.Status,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    IdType = requestType.IdType,

                }
                ).FirstOrDefault();
            return Ok(data);
        }

        [HttpPost("Apply")]
        public IActionResult Apply(ApplyVM applyVM)
        {
            try
            {
                Request request = new Request();
                request.StartDate = applyVM.StartDate;
                request.EndDate = applyVM.EndDate;
                myContext.Requests.Add(request);
                myContext.SaveChanges();

                try
                {
                    RequestType requestType = new RequestType();
                    requestType.IdType = applyVM.IdType;
                    requestType.IdRequest = myContext.Requests.Select(r => r.Id).Max();
                    myContext.RequestTypes.Add(requestType);
                    myContext.SaveChanges();

                    try
                    {
                        RequestStatus requestStatus = new RequestStatus();
                        requestStatus.NIK = applyVM.NIK;
                        requestStatus.IdRequest = myContext.Requests.Select(r => r.Id).Max();
                        requestStatus.Status = "Unprocessed";
                        myContext.RequestStatuses.Add(requestStatus);
                        myContext.SaveChanges();

                        return Ok("Apply was successfully");
                    }
                    catch (Exception)
                    {
                        var delRequestType = myContext.RequestTypes.Find(applyVM.IdType);
                        myContext.RequestTypes.Remove(delRequestType);
                        var resultRType = myContext.SaveChanges();

                        var delRequestStatus = myContext.RequestStatuses.Find(applyVM.NIK);
                        myContext.RequestStatuses.Remove(delRequestStatus);
                        var resultRStatus = myContext.SaveChanges();

                        return StatusCode(405, new { status = HttpStatusCode.MethodNotAllowed, message = "..." });
                        throw;

                    }
                }
                catch (Exception)
                {
                    var delRequest = myContext.Requests.Find(myContext.Requests.Select(r => r.Id).Max());
                    myContext.Requests.Remove(delRequest);
                    var resultRequest = myContext.SaveChanges();


                    return StatusCode(405, new { status = HttpStatusCode.MethodNotAllowed, message = "..." });
                    throw;
                }
            }
            catch (Exception)
            {
                return StatusCode(400, new { status = HttpStatusCode.MethodNotAllowed, message = "Bad Request" });
                throw;
            }
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var person = myContext.Persons.FirstOrDefault(p => p.Email == changePasswordVM.Email);
            if (person != null)
            {
                var account = myContext.Accounts.FirstOrDefault(acc => acc.NIK == person.NIK);
                if (account != null && Hashing.VerifyPassword(changePasswordVM.OldPassword, account.Password))
                {
                    Account newAccount = new Account();
                    newAccount = myContext.Accounts.Find(person.NIK);
                    newAccount.NIK = account.NIK;
                    newAccount.Password = Hashing.HashPassword(changePasswordVM.NewPassword);
                    myContext.Accounts.Update(newAccount);
                    myContext.SaveChanges();
                    return Ok("Password Updated");
                }
                else
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Wrong Password" });
                }
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Email Not Found" });
            }
        }

        public string RandomString()
        {
            Random r = new Random();
            string[] ran = new string[8];
            string alphaNumeric = "abcdefghijklmnopqrstuvwxyz1234567890";
            char[] ch = new char[alphaNumeric.Length];


            for (int i = 0; i < alphaNumeric.Length; i++)
            {
                ch[i] = alphaNumeric[i];
            }

            for (int i = 0; i < 8; i++)
            {
                int rInt = r.Next(0, alphaNumeric.Length - 1);
                ran[i] = Convert.ToString(ch[rInt]);
            }

            return $"{ran[0]}{ran[1]}{ran[2]}{ran[3]}{ran[4]}{ran[5]}{ran[6]}{ran[7]}";
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var newPass = RandomString();
            Person person = myContext.Persons.Where(p => p.Email == forgotPasswordVM.Email).FirstOrDefault();
            if (person != null)
            {
                Account newAccount = myContext.Accounts.Find(person.NIK);
                newAccount.NIK = person.NIK;
                newAccount.Password = Hashing.HashPassword(newPass);
                myContext.Accounts.Update(newAccount);
                myContext.SaveChanges();
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("hidgastg@gmail.com", ""),
                    EnableSsl = true
                };
                client.Send("hidgastg@gmail.com", forgotPasswordVM.Email, "RESET PASSWORD REQUEST", $"Hello {person.FirstName} {person.LastName} \nHere is Your New Password : {newPass}");
                return Ok("Request Sent");
            }
            else
            {
                return NotFound("Email Not Found");
            }
        }
    }
}
