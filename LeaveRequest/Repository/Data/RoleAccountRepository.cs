using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repository.Data
{
    public class RoleAccountRepository : GeneralRepository<MyContext, RoleAccount, int>
    {
        public RoleAccountRepository(MyContext conn) : base(conn)
        {

        }
    }
}
