using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToolshopApp2.Model;
using ToolshopApp2.Connection;

namespace ToolshopApp2.Data
{
    class DatabaseConnectionContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<KindOfUser> KindOfUsers { get; set; }
        //public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<ClassyfyList> ClassyfyLists { get; set; }
        public DbSet<CostCenterList> CostCenterLists { get; set; }
        public DbSet<ProjectList> ProjectLists { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<BlockedDay> BlockedDays { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AcceptanceDate> AcceptanceDates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.connectionString);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Request>().HasMany(r => r.RequestStatuses)
        //}
    }
}
