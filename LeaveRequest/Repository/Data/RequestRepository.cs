using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repository.Data
{
    public class RequestRepository : GeneralRepository<MyContext, Request, int>
    {
        public RequestRepository(MyContext conn) : base(conn)
        {

        }
    }
}
