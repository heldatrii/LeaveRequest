using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repository.Data
{
    public class PersonRepository : GeneralRepository<MyContext, Person, string>
    {
        public PersonRepository(MyContext conn) : base(conn)
        {

        }
    }
}
