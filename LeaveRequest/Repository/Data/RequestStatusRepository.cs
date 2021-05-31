using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repository.Data
{
    public class RequestStatusRepository : GeneralRepository<MyContext, RequestStatus, string>
    {
        public RequestStatusRepository(MyContext conn) : base(conn)
        {

        }
    }
}
