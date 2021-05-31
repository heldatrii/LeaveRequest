﻿using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
        public RoleRepository(MyContext conn) : base(conn)
        {

        }
    }
}