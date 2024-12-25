using Company.DAL.Contexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Reposatories
{
	public class TaskRepository:GenericRepository<TaskMod>
	{
		public TaskRepository(CompanyDBContext dBContext) : base(dBContext) { }

		public TaskMod GetTaskByName(string name)
		{
			return dBContext.tasks.Where(t => t.Name == name).FirstOrDefault();
		}
	}
}
