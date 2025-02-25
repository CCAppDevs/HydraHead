using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraHead
{
    public interface IHttpService<T, K>
    {
        Task<T> Get(K id);
        Task<T[]> GetAll();
        Task<T> Post(T data);
        Task<T> Put(K id, T data);
        Task Delete(K id);
    }
}
