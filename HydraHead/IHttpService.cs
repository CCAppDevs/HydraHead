using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraHead
{
    public interface IHttpService<T>
    {
        T Get();
        T[] GetAll();
        T Post(T data);
        T Put(T data);
        void Delete();

    }
}
