using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HydraHead
{
    public class HydraApiService : HttpService<Event, int>
    {
        public HydraApiService(string baseUrl = "https://localhost:7101") : base(baseUrl)
        {
        }

        public async override Task Delete(int id)
        {
            using HttpResponseMessage response = await _client.DeleteAsync("/api/events/" + id);

            response.EnsureSuccessStatusCode();

            return;
        }

        public async override Task<Event> Get(int id)
        {
            using HttpResponseMessage response = await _client.GetAsync("/api/events/" + id);

            response.EnsureSuccessStatusCode();

            var evt = await response.Content.ReadFromJsonAsync<Event>();

            if (evt == null)
            {
                throw new FormatException("json failed to parse");
            }

            return evt;
        }

        public async override Task<Event[]> GetAll()
        {
            using HttpResponseMessage response = await _client.GetAsync("/api/events/");

            response.EnsureSuccessStatusCode();

            var evt = await response.Content.ReadFromJsonAsync<Event[]>();

            if (evt.Length > 0)
            {
                throw new FormatException("json failed to parse");
            }

            return evt;
        }

        public async override Task<Event> Post(Event data)
        {
            using HttpResponseMessage response = await _client.PostAsJsonAsync("/api/events/", data);

            response.EnsureSuccessStatusCode();

            var evt = await response.Content.ReadFromJsonAsync<Event>();

            if (evt == null)
            {
                throw new FormatException("json failed to parse");
            }

            return evt;
        }

        public async override Task<Event> Put(int id, Event data)
        {
            throw new NotImplementedException();
        }
    }
}
