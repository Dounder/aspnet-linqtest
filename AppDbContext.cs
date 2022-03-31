﻿using LinqCrudTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinqCrudTest
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Positions> Positions { get; set; }
    }
}
