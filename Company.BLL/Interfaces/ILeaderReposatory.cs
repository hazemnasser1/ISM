using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Models;

namespace Company.BLL.Interfaces
{
    public interface ILeaderReposatory
    {
        List<Member>? showteam(int projectID);
        void AddTask(TaskMod task);
        Leader GetByEmail(string email);
        void UpdateTask(TaskMod task);
        void addTeamMember(Member member,Leader leader);
    }
}
