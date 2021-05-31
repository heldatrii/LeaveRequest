﻿using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repository.Data
{
    public class DepartementRepository : GeneralRepository<MyContext, Departement, int>
    {
        public DepartementRepository(MyContext conn) : base(conn)
        {

        }
    }
}