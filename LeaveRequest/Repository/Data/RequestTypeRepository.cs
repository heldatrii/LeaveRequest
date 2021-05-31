using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repository.Data
{
    public class RequestTypeRepository : GeneralRepository<MyContext, RequestType, int>
    {
        public RequestTypeRepository(MyContext conn) : base(conn)
        {

        }
    }
}
