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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //account ke role many to many 
            modelBuilder.Entity<RoleAccount>()
                .HasKey(ra => new { ra.IdAccount, ra.IdRole });
            modelBuilder.Entity<RoleAccount>()
                .HasOne(ra => ra.Account)
                .WithMany(a => a.RoleAccounts)
                .HasForeignKey(ra => ra.IdAccount);
            modelBuilder.Entity<RoleAccount>()
                .HasOne(ra => ra.Role)
                .WithMany(r => r.RoleAccounts)
                .HasForeignKey(ra => ra.IdRole);

            //account ke role many to many 
            modelBuilder.Entity<RequestType>()
                .HasKey(rt => new { rt.IdRequest, rt.IdType });
            modelBuilder.Entity<RequestType>()
                .HasOne(rt => rt.Request)
                .WithMany(b => b.RequestTypes)
                .HasForeignKey(rt => rt.IdType);
            modelBuilder.Entity<RequestType>()
                .HasOne(rt => rt.Tipe)
                .WithMany(b => b.RequestTypes)
                .HasForeignKey(rt => rt.IdType);

            //selfjoin
            modelBuilder.Entity<Person>()
                .HasMany(p => p.subPerson)
                .WithOne(p => p.ParentPerson)
                .HasForeignKey(p => p.ManagerId);
        }

    }
}
