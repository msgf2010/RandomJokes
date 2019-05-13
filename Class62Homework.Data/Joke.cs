using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Class62Homework.Data
{
    public class Joke
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("id")]
        public int WebsiteId { get; set; }
        
        public string Setup { get; set; }
        public string PunchLine { get; set; }

        [JsonIgnore]
        public List<UserLikedJokes> UserLikedJokes { get; set; }
    }
}
