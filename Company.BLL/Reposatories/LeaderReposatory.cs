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
    internal class LeaderReposatory : IGenaricReposatory<Leader>, ILeaderReposatory
    {
        private CompanyDBContext dBContext;
        public LeaderReposatory(CompanyDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public int AddTask(TaskMod task)
        {
            dBContext.tasks.Add(task);
            Project project = dBContext.Find<Project>(task.project.Id);
            project.tasks.Add(task);
            return dBContext.SaveChanges();
        }

        public int addTeamMember(Member member,Leader leader)
        {
            leader.Project.members.Add(member);
            return dBContext.SaveChanges();
        }

        public int Delete(Leader member)
        {
            dBContext.leaders.Remove(member);
            return dBContext.SaveChanges();
        }

        public Leader Get(int id)
        {
            return dBContext.Find<Leader>(id);

        }

        public IEnumerable<Leader> GetAll()
        {
            return dBContext.leaders.ToList();
        }

        public int Insert(Leader member)
        {
            dBContext.leaders.Add(member);
            return dBContext.SaveChanges();
        }

        public List<Member>? showteam(int projectID)
        {
            Project project= dBContext.Find<Project>(projectID);
            return project.members.ToList();
        }

        public int Update(Leader member)
        {
            dBContext.leaders.Update(member);
            return dBContext.SaveChanges();
        }

        public int UpdateTask(TaskMod task)
        {
            dBContext.tasks.Update(task);
            return dBContext.SaveChanges();
        }
    }
}
