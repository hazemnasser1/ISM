using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Configurations
{
    public class LeaderConfig : IEntityTypeConfiguration<Leader>
    {
        public void Configure(EntityTypeBuilder<Leader> builder)
        {
           // builder
           //.HasOne(l => l.Project)
           //.WithOne()
           //.HasForeignKey<Project>(p => p.LeaderId);
        }
    }
}
