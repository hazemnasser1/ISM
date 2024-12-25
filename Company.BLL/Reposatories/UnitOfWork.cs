using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Reposatories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly CompanyDBContext dBContext;
		public LeaderReposatory LeaderReposatory { get; set; }
		public MemberReposatory MemberReposatory { get; set; }
		public TaskRepository TaskRepository { get; set; }

		public UnitOfWork(CompanyDBContext companyDB)
		{
			dBContext = companyDB;
			TaskRepository = new TaskRepository(dBContext);
			LeaderReposatory = new LeaderReposatory(dBContext);
			MemberReposatory = new MemberReposatory(dBContext);


		}

		public int Complete()
		{
			return dBContext.SaveChanges();
		}

		public void Dispose()
		{
			dBContext.Dispose();
		}
	}
}
