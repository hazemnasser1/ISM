using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Reposatories
{
	public class GenericRepository<T> : IGenaricReposatory<T> where T : BaseEntity
	{
		public readonly CompanyDBContext dBContext;
		public GenericRepository(CompanyDBContext dbContext)
		{
			dBContext = dbContext;
		}
		public void Delete(T member)
		{
			dBContext.Set<T>().Remove(member);
		}

		public T Get(int id)
		{
			return dBContext.Set<T>().Where(m=>m.Id == id).FirstOrDefault();
		}

		public IEnumerable<T> GetAll()
		{
			return dBContext.Set<T>().ToList();
		}

		public void Insert(T member)
		{
			dBContext.Set<T>().Add(member);
		}

		public void Update(T member)
		{
			dBContext.Set<T>().Update(member);
		}
	}
}
