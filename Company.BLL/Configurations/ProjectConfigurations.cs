using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Configurations
{
    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            //builder.HasOne(p => p.Leader)
            //    .WithOne(l => l.Project)
            //    .HasForeignKey<Project>(p => p.LeaderId);
        }
    }
}
