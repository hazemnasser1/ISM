using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{

    public interface IGenaricReposatory<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Insert(T member);
        void Update(T member);
        void Delete(T member);
    }
}
