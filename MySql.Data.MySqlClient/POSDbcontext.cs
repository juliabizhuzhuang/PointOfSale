using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MySql.Data.Core;

namespace MySql.Data.MySqlClient
{
    public class POSDbcontext : DbContext
    {
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Salesreturn> Salesreturn { get; set; }
        public DbSet<Salesreturnitem> Salesreturnitem { get; set; }
        public DbSet<Transactiondetails> Transactiondetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=POSDB;Integrated Security=True");
        }
    }
}