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
    internal class MemberReposatory : IMemberReposatory,IGenaricReposatory<Member>
    {
        private CompanyDBContext dBContext;
        public MemberReposatory(CompanyDBContext dBContext) {
            this.dBContext = dBContext;
        }
        public int Delete(Member member)
        {
            dBContext.members.Remove(member);
            return dBContext.SaveChanges();
        }


        public Member? Get(int id)
        {
            return dBContext.Find<Member>(id);
        }

        public IEnumerable<Member> GetAll()
        {
            return dBContext.members.ToList();
        }

        public int Insert(Member member)
        {
            dBContext.members.Add(member);
            return dBContext.SaveChanges();
        }

        public int MarkAsDone(TaskMod task)
        {
            return dBContext.SaveChanges();
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

        public int Update(Member member)
        {
            dBContext.members.Update(member);
            return dBContext.SaveChanges();
        }
    }
}
