using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repository.Data
{
    public class TipeRepository : GeneralRepository<MyContext, Tipe, int>
    {
        public TipeRepository(MyContext conn) : base(conn)
        {

        }
    }
}
