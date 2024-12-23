using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{

    internal interface IGenaricReposatory<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        int Insert(T member);
        int Update(T member);
        int Delete(T member);
    }
}
