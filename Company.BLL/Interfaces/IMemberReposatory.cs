using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Models;

namespace Company.BLL.Interfaces
{
    public interface IMemberReposatory
    {
        void MarkAsDone(TaskMod task);
        List<TaskMod>? Showtasks(Member member);
        List<Member>? showteam(int projectID);
        
    }

}
