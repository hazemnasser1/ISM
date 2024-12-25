using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;


namespace Company.BLL.Reposatories
{
    public class MemberReposatory :GenericRepository<Member>, IMemberReposatory
    {
        //private CompanyDBContext dBContext;
        public MemberReposatory(CompanyDBContext dBContext) : base(dBContext) { }

        public void MarkAsDone(TaskMod task)
        {
            task.isDone = true;
            //return dBContext.SaveChanges();
        }

        public List<TaskMod>? Showtasks(Member member)
        {
            return member.tasks;
        }

        public List<Member>? showteam(int projectID)
        {
            Project project = dBContext.Find<Project>(projectID);

            return project.members.ToList();
        }
    }
}
