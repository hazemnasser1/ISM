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
    public class LeaderReposatory : GenericRepository<Leader>, ILeaderReposatory
    {
        //private CompanyDBContext dBContext;
        public LeaderReposatory(CompanyDBContext dBContext):base(dBContext) { }

        public void AddTask(TaskMod task)
        {
            dBContext.tasks.Add(task);
            Project project = dBContext.Find<Project>(task.project.Id);
            project.tasks.Add(task);
            //return dBContext.SaveChanges();
        }

        public void addTeamMember(Member member,Leader leader)
        {
            leader.Project.members.Add(member);
            //return dBContext.SaveChanges();
        }

        public void Delete(Leader member)
        {
            dBContext.leaders.Remove(member);
            //return dBContext.SaveChanges();
        }

        public Leader GetByEmail(string email)
        {
            return dBContext.leaders.Where(m => m.Email == email).Include(l => l.members).Include(b => b.Project).FirstOrDefault();
        }

        public List<Member>? showteam(int projectID)
        {
            Project project= dBContext.Find<Project>(projectID);
            return project.members.ToList();
        }

        public void UpdateTask(TaskMod task)
        {
            dBContext.tasks.Update(task);
            //return dBContext.SaveChanges();
        }
    }
}
