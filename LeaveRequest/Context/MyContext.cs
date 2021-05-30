using LeaveRequest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<LeaveAllowance> LeaveAllowances { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleAccount> RoleAccounts { get; set; }
        public DbSet<Tipe> Tipes { get; set; }
    }
}
