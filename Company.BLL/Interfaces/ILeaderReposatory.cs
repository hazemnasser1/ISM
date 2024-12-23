using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Models;

namespace Company.BLL.Interfaces
{
    internal interface ILeaderReposatory
    {
        List<Member>? showteam(int projectID);
        int AddTask(TaskMod task);
        int UpdateTask(TaskMod task);
        int addTeamMember(Member member,Leader leader);
    }
}
