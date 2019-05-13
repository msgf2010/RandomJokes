using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace Class62Homework.Data
{
    public class JokesApi
    {
        public IEnumerable<Joke> GetRandomJoke()
        {
            using (var client = new HttpClient())
            {
                var json = client.GetStringAsync("https://official-joke-api.appspot.com/jokes/programming/random").Result;

                return JsonConvert.DeserializeObject<IEnumerable<Joke>>(json);
            }

        }
    }
}