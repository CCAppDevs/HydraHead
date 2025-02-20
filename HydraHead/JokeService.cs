using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraHead
{
    public class JokeService : HttpService<Joke>
    {
        public JokeService(string baseUrl) : base(baseUrl)
        {
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override async Task<Joke> Get()
        {
            // this gets data from the api
            using HttpResponseMessage response = await _client.GetAsync("api?format=json");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            // process the json

            return new Joke();
        }

        public override Joke[] GetAll()
        {
            throw new NotImplementedException();
        }

        public override Joke Post(Joke data)
        {
            throw new NotImplementedException();
        }

        public override Joke Put(Joke data)
        {
            throw new NotImplementedException();
        }
    }
}
