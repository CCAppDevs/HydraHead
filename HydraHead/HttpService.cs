using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraHead
{
    public abstract class HttpService<T> : IHttpService<T>
    {
        protected string _baseUrl;

        protected static HttpClient _client = new();

        public HttpService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public abstract Task Delete();
        public abstract Task<T> Get();
        public abstract Task<T[]> GetAll();
        public abstract Task<T> Post(T data);
        public abstract Task<T> Put(T data);
    }
}
