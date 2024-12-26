using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;


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
            Project project = dBContext.projects.Where(p=>p.Id == projectID).Include(p=>p.members).FirstOrDefault();

            return project.members.ToList();
        }

        public Member GetMemberWithEmail(string email)
        {
            return dBContext.members.Where(m=>m.Email == email).Include(m=>m.tasks).ThenInclude(t=>t.project).Include(m => m.projects).FirstOrDefault();
        }

        public void RemoveTask(TaskMod task, string email)
        {
            var member = GetMemberWithEmail(email);
            member.tasks.Remove(task);
        }
    }
}
