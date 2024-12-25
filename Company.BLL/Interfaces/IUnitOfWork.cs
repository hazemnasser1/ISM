using Company.BLL.Reposatories;
using Company.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
	public interface IUnitOfWork: IDisposable
	{
		//private readonly CompanyDBContext dBContext;
		public LeaderReposatory LeaderReposatory { get; set; }
		public MemberReposatory MemberReposatory { get; set; }

		int Complete();
		void Dispose();
	}
}
