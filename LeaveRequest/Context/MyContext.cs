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
                .HasKey(ra => new { ra.NIK, ra.IdRole });
            modelBuilder.Entity<RoleAccount>()
                .HasOne(ra => ra.Account)
                .WithMany(a => a.RoleAccounts)
                .HasForeignKey(ra => ra.NIK);
            modelBuilder.Entity<RoleAccount>()
                .HasOne(ra => ra.Role)
                .WithMany(r => r.RoleAccounts)
                .HasForeignKey(ra => ra.IdRole);

            //request ke tipe many to many 
            modelBuilder.Entity<RequestType>()
                .HasKey(rt => new { rt.RequestId, rt.TipeId });
            modelBuilder.Entity<RequestType>()
                .HasOne(rt => rt.Request)
                .WithMany(b => b.RequestTypes)
                .HasForeignKey(rt => rt.RequestId);
            modelBuilder.Entity<RequestType>()
                .HasOne(rt => rt.Tipe)
                .WithMany(b => b.RequestTypes)
                .HasForeignKey(rt => rt.TipeId);
            
            //person ke request many to many 
            modelBuilder.Entity<RequestStatus>()
                .HasKey(rt => new { rt.RequestId, rt.NIK });
            modelBuilder.Entity<RequestStatus>()
                .HasOne(rt => rt.Person)
                .WithMany(b => b.RequestStatuses)
                .HasForeignKey(rt => rt.NIK);
            modelBuilder.Entity<RequestStatus>()
                .HasOne(rt => rt.Request)
                .WithMany(b => b.RequestStatuses)
                .HasForeignKey(rt => rt.RequestId);

            //selfjoin
            modelBuilder.Entity<Person>()
                .HasMany(p => p.subPerson)
                .WithOne(p => p.ParentPerson)
                .HasForeignKey(p => p.ManagerId);
        }

    }
}
