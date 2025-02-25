using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraHead
{
    public interface IHttpService<T>
    {
        Task<T> Get();
        Task<T[]> GetAll();
        Task<T> Post(T data);
        Task<T> Put(T data);
        Task Delete();
    }
}
