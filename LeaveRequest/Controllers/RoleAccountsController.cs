using LeaveRequest.Base;
using LeaveRequest.Models;
using LeaveRequest.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAccountsController : BaseController<RoleAccount, RoleAccountRepository, int>
    {
        public RoleAccountsController(RoleAccountRepository roleAccountRepository) : base(roleAccountRepository)
        {

        }
    }
}
