using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.DAL.Contexts
{
    public class CompanyDBContext:DbContext
    {
        public CompanyDBContext(DbContextOptions<CompanyDBContext> options): base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        //    optionsBuilder.UseSqlServer("Server=.;database=companyDB;trusted_connection=true");

        public DbSet<Member>? members { get; set; }
        public DbSet<Leader>? leaders { get; set; }

        public DbSet<TaskMod>? tasks { get; set; }

        public DbSet<Project>? projects { get; set; }
        
    }
}
