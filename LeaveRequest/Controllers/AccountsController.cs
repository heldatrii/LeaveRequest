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

                throw;
            }
        }
    }
}
