using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraHead
{
    public abstract class HttpService<T, K> : IHttpService<T, K>
    {
        public string _baseUrl;

        protected static HttpClient _client = new();

        public HttpService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public abstract Task Delete(K id);
        public abstract Task<T> Get(K id);
        public abstract Task<T[]> GetAll();
        public abstract Task<T> Post(T data);
        public abstract Task<T> Put(K id, T data);
    }
}
