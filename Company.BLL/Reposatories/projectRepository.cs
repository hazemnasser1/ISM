using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Company.BLL.Reposatories
{
    public class projectRepository : GenericRepository<Project>
    {
        public projectRepository(CompanyDBContext dbContext) : base(dbContext)
        {   
              
        }
        public Project GetByIDWithInclude(int id)
        {
            return dBContext.projects.Where(p => p.Id == id).Include(p => p.members).Include(p => p.tasks).FirstOrDefault();
            

        }
    }
}
