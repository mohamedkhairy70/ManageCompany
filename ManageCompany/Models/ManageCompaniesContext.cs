using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageCompany.Models;

namespace ManageCompany.Models
{
    public class ManageCompanyContext : DbContext
    {
        public ManageCompanyContext(DbContextOptions<ManageCompanyContext> options) : base (options)
        { }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
