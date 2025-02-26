using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HydraHead
{
    public class JokeService : HttpService<Joke, int>
    {
        public JokeService(string baseUrl) : base(baseUrl)
        {
        }

        public override Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<Joke> Get(int id = 0)
        {
            // this gets data from the api
            using HttpResponseMessage response = await _client.GetAsync("/api?format=json");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            // process the json

            var joke = JsonSerializer.Deserialize<Joke>(jsonResponse);

            if (joke == null)
            {
                throw new JsonException("could not deserialize json response");
            }

            return joke;
        }

        public override Task<Joke[]> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<Joke> Post(Joke data)
        {
            throw new NotImplementedException();
        }

        public override Task<Joke> Put(int id, Joke data)
        {
            throw new NotImplementedException();
        }
    }
}
