using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repository.Data
{
    public class LeaveAllowanceRepository : GeneralRepository<MyContext, LeaveAllowance, string>
    {
        public LeaveAllowanceRepository(MyContext conn) : base(conn)
        {

        }
    }
}
