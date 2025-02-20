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

        public abstract void Delete();
        public abstract T Get();
        public abstract T[] GetAll();
        public abstract T Post(T data);
        public abstract T Put(T data);
    }
}
